module Womb.Backends.SDL.Api.Functions

open System
open System.Runtime.InteropServices
open System.Text
open Womb.Backends.SDL.Api.Constants
open Womb.Backends.SDL.Api.Structs

// ---------------------------------------------------------------------------
// Native library name
// SDL2 ships as "SDL2" on Windows, "SDL2" on macOS, "SDL2" on Linux.
// The CLR resolves libSDL2.so / SDL2.dll / libSDL2.dylib automatically.
// ---------------------------------------------------------------------------

[<Literal>]
let private DllName = "SDL2"

// ---------------------------------------------------------------------------
// Initialization / shutdown
// ---------------------------------------------------------------------------

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern int SDL_Init(uint32 flags)

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern void SDL_Quit()

// ---------------------------------------------------------------------------
// Window management
// ---------------------------------------------------------------------------

[<DllImport(DllName, EntryPoint = "SDL_CreateWindow", CallingConvention = CallingConvention.Cdecl)>]
extern nativeint private INTERNAL_SDL_CreateWindow(
    byte[] title,
    int x,
    int y,
    int w,
    int h,
    SDL_WindowFlags flags)

/// Create a window with a UTF-8 title string.
let SDL_CreateWindow (title: string) x y w h (flags: SDL_WindowFlags) =
    INTERNAL_SDL_CreateWindow(
        Encoding.UTF8.GetBytes(title + "\000"),
        x, y, w, h, flags)

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern void SDL_DestroyWindow(nativeint window)

// ---------------------------------------------------------------------------
// OpenGL context
// ---------------------------------------------------------------------------

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern nativeint SDL_GL_CreateContext(nativeint window)

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern int SDL_GL_MakeCurrent(nativeint window, nativeint context)

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern void SDL_GL_SwapWindow(nativeint window)

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern void SDL_GL_DeleteContext(nativeint context)

/// Raw P/Invoke: accepts a native pointer to the proc name (nativeint -> nativeint).
/// This matches the ProcAddressHandler delegate in the OpenGL backend.
[<DllImport(DllName, EntryPoint = "SDL_GL_GetProcAddress", CallingConvention = CallingConvention.Cdecl)>]
extern nativeint SDL_GL_GetProcAddress(nativeint proc)

[<DllImport(DllName, EntryPoint = "SDL_GL_GetProcAddress", CallingConvention = CallingConvention.Cdecl)>]
extern nativeint private INTERNAL_SDL_GL_GetProcAddressBytes(byte[] proc)

/// String-convenience wrapper: looks up an OpenGL function pointer by name.
let SDL_GL_GetProcAddressStr (proc: string) : nativeint =
    INTERNAL_SDL_GL_GetProcAddressBytes(Encoding.UTF8.GetBytes(proc + "\000"))

// ---------------------------------------------------------------------------
// GL attributes
// ---------------------------------------------------------------------------

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern int SDL_GL_SetAttribute(SDL_GLattr attr, int value)

/// Convenience overload for SDL_GLprofile values.
let SDL_GL_SetAttributeProfile (attr: SDL_GLattr) (profile: SDL_GLprofile) =
    SDL_GL_SetAttribute(attr, int profile)

/// Convenience overload for SDL_GLcontext flag values.
let SDL_GL_SetAttributeContext (attr: SDL_GLattr) (flags: SDL_GLcontext) =
    SDL_GL_SetAttribute(attr, int flags)

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern int SDL_GL_GetAttribute(SDL_GLattr attr, int& value)

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern int SDL_GL_SetSwapInterval(int interval)

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern int SDL_GL_GetSwapInterval()

// ---------------------------------------------------------------------------
// Events
// ---------------------------------------------------------------------------

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern int SDL_PollEvent(SDL_Event& event)

[<DllImport(DllName, CallingConvention = CallingConvention.Cdecl)>]
extern void SDL_PumpEvents()

// ---------------------------------------------------------------------------
// Input / mouse
// ---------------------------------------------------------------------------

[<DllImport(DllName, EntryPoint = "SDL_GetMouseState", CallingConvention = CallingConvention.Cdecl)>]
extern uint32 private INTERNAL_SDL_GetMouseState(int& x, int& y)

/// Returns (buttonMask, x, y).
let SDL_GetMouseState () =
    let mutable x = 0
    let mutable y = 0
    let mask = INTERNAL_SDL_GetMouseState(&x, &y)
    (mask, x, y)

// ---------------------------------------------------------------------------
// Error handling
// ---------------------------------------------------------------------------

[<DllImport(DllName, EntryPoint = "SDL_GetError", CallingConvention = CallingConvention.Cdecl)>]
extern nativeint private INTERNAL_SDL_GetError()

let SDL_GetError () =
    Marshal.PtrToStringUTF8(INTERNAL_SDL_GetError())
