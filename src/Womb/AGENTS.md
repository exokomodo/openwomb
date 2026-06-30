# AGENTS.md — Womb F# Library

Guidance for AI agents (and humans) working on the Womb library, particularly on writing native P/Invoke bindings.

---

## Project Structure

```
src/Womb/
  Backends/
    OpenGL/
      Api/
        Constants/        # One file per GL version (OpenGL1_0.fs … OpenGL3_3.fs)
        Common.fs         # Shared helpers (function pointer loading)
        OpenGL1_0.fs …    # Per-version function wrappers
      Module.fs           # Top-level re-export, GL setup entry point
    SDL/
      Api/
        Constants.fs      # All enums, flags, constants
        Structs.fs        # P/Invoke structs (sequential + explicit-layout union)
        Functions.fs      # DllImport bindings + F#-friendly wrappers
      Module.fs           # Top-level re-export
  Graphics/
  Engine/
  Input/
  Lib/
  Game.fs
  Types.fs
  Womb.fsproj
```

When adding a new native backend (e.g. Vulkan), mirror this layout exactly.

---

## Writing F# P/Invoke Bindings

### 1. Constants and Enums

- **Module header:** `module Womb.Backends.<Backend>.Api.Constants`
- **Scalar constants** use `[<Literal>]` with explicit type annotation:

  ```fsharp
  [<Literal>]
  let SDL_INIT_VIDEO : uint32 = 0x00000020u
  ```

- **Flag enums** use `[<Flags>]` and match the underlying type of the native enum:

  ```fsharp
  [<Flags>]
  type SDL_WindowFlags =
      | SDL_WINDOW_NONE    = 0u
      | SDL_WINDOW_OPENGL  = 0x00000002u
  ```

- **Non-flag enums** omit `[<Flags>]`:

  ```fsharp
  type SDL_GLattr =
      | SDL_GL_RED_SIZE = 0
      | SDL_GL_DEPTH_SIZE = 6
  ```

- **Byte-backed enums** (e.g. `SDL_WindowEventID`) use `uy` suffix:

  ```fsharp
  type SDL_WindowEventID =
      | SDL_WINDOWEVENT_RESIZED = 5uy
  ```

- **Computed constants** (scancode-backed keycodes): derive with `|||` at definition time and use decimal literals — F# enum members must be compile-time constants:

  ```fsharp
  // SDL_SCANCODE_F1 = 58, mask = 1 <<< 30
  | SDLK_F1 = 1073741882   // 58 ||| (1 <<< 30)
  ```

- **Helper functions** (non-literal computed values) live at module level, not in enums:

  ```fsharp
  let SDL_WINDOWPOS_CENTERED_DISPLAY x = SDL_WINDOWPOS_CENTERED_MASK ||| x
  ```

---

### 2. Structs

- **Module header:** `module Womb.Backends.<Backend>.Api.Structs`
- Open `System.Runtime.InteropServices` for `StructLayout`, `FieldOffset`, `LayoutKind`.
- **Sequential structs** (most event sub-structs):

  ```fsharp
  [<Struct; StructLayout(LayoutKind.Sequential)>]
  type SDL_WindowEvent =
      val mutable ``type``    : SDL_EventType
      val mutable timestamp   : uint32
      val mutable windowID    : uint32
      val mutable windowEvent : SDL_WindowEventID
      val private _pad1       : byte   // padding must be declared to match native layout
      val private _pad2       : byte
      val private _pad3       : byte
      val mutable data1       : int32
      val mutable data2       : int32
  ```

- **Explicit-layout unions** (e.g. `SDL_Event`): use `LayoutKind.Explicit` with `[<FieldOffset(0)>]` on every field, and set `Size` to match the native struct:

  ```fsharp
  [<Struct; StructLayout(LayoutKind.Explicit, Size = 56)>]
  type SDL_Event =
      [<FieldOffset(0)>] val mutable ``type``  : SDL_EventType
      [<FieldOffset(0)>] val mutable window    : SDL_WindowEvent
      [<FieldOffset(0)>] val mutable key       : SDL_KeyboardEvent
      [<FieldOffset(0)>] val mutable quit      : SDL_QuitEvent
  ```

- **Padding fields** must match the native struct layout exactly. Check the C header or the reference C# bindings to find padding bytes.
- **Reserved F# keywords** used as field names (e.g. `type`) require backtick escaping: `` ``type`` ``.
- **Native pointers** (`void*`, `SDL_Window*`, `SDL_GLContext`, etc.) map to `nativeint`.
- **Strings from native code** — use `nativeint` + `Marshal.PtrToStringUTF8()` rather than `string` directly in the P/Invoke signature.

---

### 3. Functions (DllImport)

- **Module header:** `module Womb.Backends.<Backend>.Api.Functions`
- **DLL name constant:**

  ```fsharp
  [<Literal>]
  let private DllName = "SDL2"   // CLR resolves libSDL2.so / SDL2.dll / libSDL2.dylib
  ```

- **Simple extern:**

  ```fsharp
  [<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
  extern int SDL_Init(uint32 flags)
  ```

- **String parameters** — never pass `string` directly; marshal via `byte[]` + UTF-8 encoding. Use a private `INTERNAL_` extern and a public F# wrapper:

  ```fsharp
  [<DllImport(DllName, EntryPoint = "SDL_GL_GetProcAddress", CallingConvention = CallingConvention.Cdecl)>]
  extern nativeint private INTERNAL_SDL_GL_GetProcAddress(byte[] proc)

  let SDL_GL_GetProcAddress (proc: string) =
      INTERNAL_SDL_GL_GetProcAddress(Encoding.UTF8.GetBytes(proc + "\000"))
  ```

  Always append `"\000"` to null-terminate the byte array.

- **Out parameters / byref** — use `&` in the extern signature:

  ```fsharp
  [<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
  extern int SDL_PollEvent(SDL_Event& event)
  ```

  Call site: `SDL_PollEvent(&event)`

- **Enum parameters** — pass enums directly; the CLR handles the underlying integer mapping:

  ```fsharp
  [<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
  extern int SDL_GL_SetAttribute(SDL_GLattr attr, int value)
  ```

  When the native function takes `int` but the call site has an enum, cast explicitly at the call site: `int SDL_GLprofile.SDL_GL_CONTEXT_PROFILE_CORE`.

- **Returning strings from native** — return `nativeint` and wrap with `Marshal.PtrToStringUTF8`:

  ```fsharp
  [<DllImport(DllName, EntryPoint = "SDL_GetError", CallingConvention = CallingConvention.Cdecl)>]
  extern nativeint private INTERNAL_SDL_GetError()

  let SDL_GetError () = Marshal.PtrToStringUTF8(INTERNAL_SDL_GetError())
  ```

- **Multi-out wrappers** — wrap functions with multiple `out` params into a tuple-returning F# function:

  ```fsharp
  let SDL_GetMouseState () =
      let mutable x = 0
      let mutable y = 0
      let mask = SDL_GetMouseState(&x, &y)
      (mask, x, y)
  ```

---

### 4. Wiring into the Project File

Add new compile items to `Womb.fsproj` **in dependency order** — a module must appear before anything that opens it. The SDL backend must appear before `Logging.fs` since engine modules open it.

```xml
<!-- SDL (must precede all engine code) -->
<Compile Include="Backends/SDL/Api/Constants.fs" />
<Compile Include="Backends/SDL/Api/Structs.fs" />
<Compile Include="Backends/SDL/Api/Functions.fs" />
<Compile Include="Backends/SDL/Module.fs" />
<!-- Logging -->
<Compile Include="Logging.fs" />
```

For a new backend like Vulkan, add its files in the same position relative to the engine modules that consume it.

---

### 5. Updating Engine Call Sites

When replacing a C#-based binding with F# bindings:

1. Replace `open SDL2Bindings` (or similar) with `open Womb.Backends.SDL.Api.Constants`, `.Structs`, `.Functions`.
2. Strip the C# namespace prefix from all call sites: `SDL.SDL_Init(...)` → `SDL_Init(...)`.
3. Fix C#-style casts: `(int)SomeEnum.Value` → `int SomeEnum.Value`.
4. Enum values that were passed as a different type in C# may need explicit `int` casts at the F# call site.

---

### 6. Conventions

- **Naming:** match the native SDL/GL/Vulkan symbol names exactly — no F# casing conventions.
- **Scope:** bind only what the engine uses. Add more bindings incrementally as features require them.
- **No silent failures:** if an `extern` can return an error code, expose it — don't swallow it in the wrapper.
- **No global mutable state** in bindings modules.
- **`--warnaserror+` is on** — all warnings are errors. Fix them, don't suppress them.
