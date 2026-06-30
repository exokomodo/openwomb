/// SDL2 backend for Womb.
/// Re-exports constants, types, structs, and functions from the Api sub-modules.
module Womb.Backends.SDL.Module

// Re-export everything so callers can open a single module.
// The individual Api.* modules remain importable for fine-grained access.

[<AutoOpen>]
module Constants =
    let inline SDL_INIT_TIMER         () = Womb.Backends.SDL.Api.Constants.SDL_INIT_TIMER
    let inline SDL_INIT_AUDIO         () = Womb.Backends.SDL.Api.Constants.SDL_INIT_AUDIO
    let inline SDL_INIT_VIDEO         () = Womb.Backends.SDL.Api.Constants.SDL_INIT_VIDEO
    let inline SDL_INIT_EVERYTHING    () = Womb.Backends.SDL.Api.Constants.SDL_INIT_EVERYTHING
    let inline SDL_WINDOWPOS_CENTERED () = Womb.Backends.SDL.Api.Constants.SDL_WINDOWPOS_CENTERED
    let inline SDL_WINDOWPOS_UNDEFINED () = Womb.Backends.SDL.Api.Constants.SDL_WINDOWPOS_UNDEFINED
