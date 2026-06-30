module Womb.Backends.SDL.Api.Structs

#nowarn "9" // Unverifiable IL due to explicit-layout union fields

open System
open System.Runtime.InteropServices
open Womb.Backends.SDL.Api.Constants

// ---------------------------------------------------------------------------
// SDL_Keysym
// ---------------------------------------------------------------------------

[<Struct; StructLayout(LayoutKind.Sequential)>]
type SDL_Keysym =
    val mutable scancode : int           // SDL_Scancode (int32)
    val mutable sym      : SDL_Keycode   // SDL_Keycode  (int32)
    val mutable ``mod``  : uint16        // SDL_Keymod
    val mutable unicode  : uint32        // deprecated

// ---------------------------------------------------------------------------
// Sub-event structs
// ---------------------------------------------------------------------------

[<Struct; StructLayout(LayoutKind.Sequential)>]
type SDL_GenericEvent =
    val mutable ``type``  : SDL_EventType
    val mutable timestamp : uint32

[<Struct; StructLayout(LayoutKind.Sequential)>]
type SDL_WindowEvent =
    val mutable ``type``    : SDL_EventType
    val mutable timestamp   : uint32
    val mutable windowID    : uint32
    val mutable windowEvent : SDL_WindowEventID
    val private _pad1       : byte
    val private _pad2       : byte
    val private _pad3       : byte
    val mutable data1       : int32
    val mutable data2       : int32

[<Struct; StructLayout(LayoutKind.Sequential)>]
type SDL_KeyboardEvent =
    val mutable ``type``  : SDL_EventType
    val mutable timestamp : uint32
    val mutable windowID  : uint32
    val mutable state     : byte
    val mutable repeat    : byte
    val private _pad2     : byte
    val private _pad3     : byte
    val mutable keysym    : SDL_Keysym

[<Struct; StructLayout(LayoutKind.Sequential)>]
type SDL_MouseMotionEvent =
    val mutable ``type``  : SDL_EventType
    val mutable timestamp : uint32
    val mutable windowID  : uint32
    val mutable which     : uint32
    val mutable state     : byte
    val private _pad1     : byte
    val private _pad2     : byte
    val private _pad3     : byte
    val mutable x         : int32
    val mutable y         : int32
    val mutable xrel      : int32
    val mutable yrel      : int32

[<Struct; StructLayout(LayoutKind.Sequential)>]
type SDL_MouseButtonEvent =
    val mutable ``type``  : SDL_EventType
    val mutable timestamp : uint32
    val mutable windowID  : uint32
    val mutable which     : uint32
    val mutable button    : byte
    val mutable state     : byte
    val mutable clicks    : byte
    val private _pad1     : byte
    val mutable x         : int32
    val mutable y         : int32

[<Struct; StructLayout(LayoutKind.Sequential)>]
type SDL_QuitEvent =
    val mutable ``type``  : SDL_EventType
    val mutable timestamp : uint32

// ---------------------------------------------------------------------------
// SDL_Event (explicit layout union, 56 bytes)
// ---------------------------------------------------------------------------

[<Struct; StructLayout(LayoutKind.Explicit, Size = 56)>]
type SDL_Event =
    [<FieldOffset(0)>] val mutable ``type``   : SDL_EventType
    // typeFSharp alias — same offset, same field, kept for source compat
    [<FieldOffset(0)>] val mutable typeFSharp : SDL_EventType
    [<FieldOffset(0)>] val mutable window     : SDL_WindowEvent
    [<FieldOffset(0)>] val mutable key        : SDL_KeyboardEvent
    [<FieldOffset(0)>] val mutable motion     : SDL_MouseMotionEvent
    [<FieldOffset(0)>] val mutable button     : SDL_MouseButtonEvent
    [<FieldOffset(0)>] val mutable quit       : SDL_QuitEvent
