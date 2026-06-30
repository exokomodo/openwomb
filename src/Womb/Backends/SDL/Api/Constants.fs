module Womb.Backends.SDL.Api.Constants

open System

// ---------------------------------------------------------------------------
// Initialization flags
// ---------------------------------------------------------------------------

[<Literal>]
let SDL_INIT_TIMER : uint32 = 0x00000001u
[<Literal>]
let SDL_INIT_AUDIO : uint32 = 0x00000010u
[<Literal>]
let SDL_INIT_VIDEO : uint32 = 0x00000020u
[<Literal>]
let SDL_INIT_JOYSTICK : uint32 = 0x00000200u
[<Literal>]
let SDL_INIT_HAPTIC : uint32 = 0x00001000u
[<Literal>]
let SDL_INIT_GAMECONTROLLER : uint32 = 0x00002000u
[<Literal>]
let SDL_INIT_EVENTS : uint32 = 0x00004000u
[<Literal>]
let SDL_INIT_EVERYTHING : uint32 = 0x0000FFFFu

// ---------------------------------------------------------------------------
// Window position
// ---------------------------------------------------------------------------

[<Literal>]
let SDL_WINDOWPOS_UNDEFINED_MASK : int = 0x1FFF0000
[<Literal>]
let SDL_WINDOWPOS_UNDEFINED : int = 0x1FFF0000
[<Literal>]
let SDL_WINDOWPOS_CENTERED_MASK : int = 0x2FFF0000
[<Literal>]
let SDL_WINDOWPOS_CENTERED : int = 0x2FFF0000

let SDL_WINDOWPOS_UNDEFINED_DISPLAY x = SDL_WINDOWPOS_UNDEFINED_MASK ||| x
let SDL_WINDOWPOS_CENTERED_DISPLAY x  = SDL_WINDOWPOS_CENTERED_MASK ||| x
let SDL_WINDOWPOS_ISUNDEFINED x = (x &&& 0xFFFF0000) = SDL_WINDOWPOS_UNDEFINED_MASK
let SDL_WINDOWPOS_ISCENTERED x  = (x &&& 0xFFFF0000) = SDL_WINDOWPOS_CENTERED_MASK

// ---------------------------------------------------------------------------
// SDL_WindowFlags
// ---------------------------------------------------------------------------

[<Flags>]
type SDL_WindowFlags =
    | SDL_WINDOW_NONE                  = 0u
    | SDL_WINDOW_FULLSCREEN            = 0x00000001u
    | SDL_WINDOW_OPENGL                = 0x00000002u
    | SDL_WINDOW_SHOWN                 = 0x00000004u
    | SDL_WINDOW_HIDDEN                = 0x00000008u
    | SDL_WINDOW_BORDERLESS            = 0x00000010u
    | SDL_WINDOW_RESIZABLE             = 0x00000020u
    | SDL_WINDOW_MINIMIZED             = 0x00000040u
    | SDL_WINDOW_MAXIMIZED             = 0x00000080u
    | SDL_WINDOW_MOUSE_GRABBED         = 0x00000100u
    | SDL_WINDOW_INPUT_FOCUS           = 0x00000200u
    | SDL_WINDOW_MOUSE_FOCUS           = 0x00000400u
    | SDL_WINDOW_FOREIGN               = 0x00000800u
    | SDL_WINDOW_FULLSCREEN_DESKTOP    = 0x00001001u
    | SDL_WINDOW_ALLOW_HIGHDPI         = 0x00002000u
    | SDL_WINDOW_MOUSE_CAPTURE         = 0x00004000u
    | SDL_WINDOW_ALWAYS_ON_TOP         = 0x00008000u
    | SDL_WINDOW_SKIP_TASKBAR          = 0x00010000u
    | SDL_WINDOW_UTILITY               = 0x00020000u
    | SDL_WINDOW_TOOLTIP               = 0x00040000u
    | SDL_WINDOW_POPUP_MENU            = 0x00080000u
    | SDL_WINDOW_KEYBOARD_GRABBED      = 0x00100000u
    | SDL_WINDOW_VULKAN                = 0x10000000u
    | SDL_WINDOW_METAL                 = 0x02000000u

// ---------------------------------------------------------------------------
// SDL_GLattr
// ---------------------------------------------------------------------------

type SDL_GLattr =
    | SDL_GL_RED_SIZE                      = 0
    | SDL_GL_GREEN_SIZE                    = 1
    | SDL_GL_BLUE_SIZE                     = 2
    | SDL_GL_ALPHA_SIZE                    = 3
    | SDL_GL_BUFFER_SIZE                   = 4
    | SDL_GL_DOUBLEBUFFER                  = 5
    | SDL_GL_DEPTH_SIZE                    = 6
    | SDL_GL_STENCIL_SIZE                  = 7
    | SDL_GL_ACCUM_RED_SIZE                = 8
    | SDL_GL_ACCUM_GREEN_SIZE              = 9
    | SDL_GL_ACCUM_BLUE_SIZE               = 10
    | SDL_GL_ACCUM_ALPHA_SIZE              = 11
    | SDL_GL_STEREO                        = 12
    | SDL_GL_MULTISAMPLEBUFFERS            = 13
    | SDL_GL_MULTISAMPLESAMPLES            = 14
    | SDL_GL_ACCELERATED_VISUAL            = 15
    | SDL_GL_RETAINED_BACKING              = 16
    | SDL_GL_CONTEXT_MAJOR_VERSION         = 17
    | SDL_GL_CONTEXT_MINOR_VERSION         = 18
    | SDL_GL_CONTEXT_EGL                   = 19
    | SDL_GL_CONTEXT_FLAGS                 = 20
    | SDL_GL_CONTEXT_PROFILE_MASK          = 21
    | SDL_GL_SHARE_WITH_CURRENT_CONTEXT    = 22
    | SDL_GL_FRAMEBUFFER_SRGB_CAPABLE      = 23
    | SDL_GL_CONTEXT_RELEASE_BEHAVIOR      = 24
    | SDL_GL_CONTEXT_RESET_NOTIFICATION    = 25
    | SDL_GL_CONTEXT_NO_ERROR              = 26

// ---------------------------------------------------------------------------
// SDL_GLprofile
// ---------------------------------------------------------------------------

[<Flags>]
type SDL_GLprofile =
    | SDL_GL_CONTEXT_PROFILE_CORE          = 0x0001
    | SDL_GL_CONTEXT_PROFILE_COMPATIBILITY = 0x0002
    | SDL_GL_CONTEXT_PROFILE_ES            = 0x0004

// ---------------------------------------------------------------------------
// SDL_GLcontext flags
// ---------------------------------------------------------------------------

[<Flags>]
type SDL_GLcontext =
    | SDL_GL_CONTEXT_DEBUG_FLAG              = 0x0001
    | SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG = 0x0002
    | SDL_GL_CONTEXT_ROBUST_ACCESS_FLAG      = 0x0004
    | SDL_GL_CONTEXT_RESET_ISOLATION_FLAG    = 0x0008

// ---------------------------------------------------------------------------
// SDL_EventType
// ---------------------------------------------------------------------------

type SDL_EventType =
    | SDL_FIRSTEVENT                   = 0u
    | SDL_QUIT                         = 0x100u
    | SDL_APP_TERMINATING              = 0x101u
    | SDL_APP_LOWMEMORY                = 0x102u
    | SDL_APP_WILLENTERBACKGROUND      = 0x103u
    | SDL_APP_DIDENTERBACKGROUND       = 0x104u
    | SDL_APP_WILLENTERFOREGROUND      = 0x105u
    | SDL_APP_DIDENTERFOREGROUND       = 0x106u
    | SDL_LOCALECHANGED                = 0x107u
    | SDL_DISPLAYEVENT                 = 0x150u
    | SDL_WINDOWEVENT                  = 0x200u
    | SDL_SYSWMEVENT                   = 0x201u
    | SDL_KEYDOWN                      = 0x300u
    | SDL_KEYUP                        = 0x301u
    | SDL_TEXTEDITING                  = 0x302u
    | SDL_TEXTINPUT                    = 0x303u
    | SDL_KEYMAPCHANGED                = 0x304u
    | SDL_MOUSEMOTION                  = 0x400u
    | SDL_MOUSEBUTTONDOWN              = 0x401u
    | SDL_MOUSEBUTTONUP                = 0x402u
    | SDL_MOUSEWHEEL                   = 0x403u
    | SDL_JOYAXISMOTION                = 0x600u
    | SDL_JOYBALLMOTION                = 0x601u
    | SDL_JOYHATMOTION                 = 0x602u
    | SDL_JOYBUTTONDOWN                = 0x603u
    | SDL_JOYBUTTONUP                  = 0x604u
    | SDL_JOYDEVICEADDED               = 0x605u
    | SDL_JOYDEVICEREMOVED             = 0x606u
    | SDL_CONTROLLERAXISMOTION         = 0x650u
    | SDL_CONTROLLERBUTTONDOWN         = 0x651u
    | SDL_CONTROLLERBUTTONUP           = 0x652u
    | SDL_CONTROLLERDEVICEADDED        = 0x653u
    | SDL_CONTROLLERDEVICEREMOVED      = 0x654u
    | SDL_CONTROLLERDEVICEREMAPPED     = 0x655u
    | SDL_FINGERDOWN                   = 0x700u
    | SDL_FINGERUP                     = 0x701u
    | SDL_FINGERMOTION                 = 0x702u
    | SDL_DOLLARGESTURE                = 0x800u
    | SDL_DOLLARRECORD                 = 0x801u
    | SDL_MULTIGESTURE                 = 0x802u
    | SDL_CLIPBOARDUPDATE              = 0x900u
    | SDL_DROPFILE                     = 0x1000u
    | SDL_DROPTEXT                     = 0x1001u
    | SDL_DROPBEGIN                    = 0x1002u
    | SDL_DROPCOMPLETE                 = 0x1003u
    | SDL_AUDIODEVICEADDED             = 0x1100u
    | SDL_AUDIODEVICEREMOVED           = 0x1101u
    | SDL_SENSORUPDATE                 = 0x1200u
    | SDL_RENDER_TARGETS_RESET         = 0x2000u
    | SDL_RENDER_DEVICE_RESET          = 0x2001u
    | SDL_USEREVENT                    = 0x8000u
    | SDL_LASTEVENT                    = 0xFFFFu

// ---------------------------------------------------------------------------
// SDL_WindowEventID
// ---------------------------------------------------------------------------

type SDL_WindowEventID =
    | SDL_WINDOWEVENT_NONE         = 0uy
    | SDL_WINDOWEVENT_SHOWN        = 1uy
    | SDL_WINDOWEVENT_HIDDEN       = 2uy
    | SDL_WINDOWEVENT_EXPOSED      = 3uy
    | SDL_WINDOWEVENT_MOVED        = 4uy
    | SDL_WINDOWEVENT_RESIZED      = 5uy
    | SDL_WINDOWEVENT_SIZE_CHANGED = 6uy
    | SDL_WINDOWEVENT_MINIMIZED    = 7uy
    | SDL_WINDOWEVENT_MAXIMIZED    = 8uy
    | SDL_WINDOWEVENT_RESTORED     = 9uy
    | SDL_WINDOWEVENT_ENTER        = 10uy
    | SDL_WINDOWEVENT_LEAVE        = 11uy
    | SDL_WINDOWEVENT_FOCUS_GAINED = 12uy
    | SDL_WINDOWEVENT_FOCUS_LOST   = 13uy
    | SDL_WINDOWEVENT_CLOSE        = 14uy
    | SDL_WINDOWEVENT_TAKE_FOCUS   = 15uy
    | SDL_WINDOWEVENT_HIT_TEST     = 16uy

// ---------------------------------------------------------------------------
// SDL_Keycode
// Printable chars use their ASCII value.
// Scancode-backed keys: (SDL_Scancode ordinal) ||| (1 <<< 30).
// ---------------------------------------------------------------------------

type SDL_Keycode =
    | SDLK_UNKNOWN       = 0
    | SDLK_RETURN        = 13
    | SDLK_ESCAPE        = 27
    | SDLK_BACKSPACE     = 8
    | SDLK_TAB           = 9
    | SDLK_SPACE         = 32
    | SDLK_EXCLAIM       = 33
    | SDLK_QUOTEDBL      = 34
    | SDLK_HASH          = 35
    | SDLK_PERCENT       = 37
    | SDLK_DOLLAR        = 36
    | SDLK_AMPERSAND     = 38
    | SDLK_LEFTPAREN     = 40
    | SDLK_RIGHTPAREN    = 41
    | SDLK_ASTERISK      = 42
    | SDLK_PLUS          = 43
    | SDLK_COMMA         = 44
    | SDLK_MINUS         = 45
    | SDLK_PERIOD        = 46
    | SDLK_SLASH         = 47
    | SDLK_0             = 48
    | SDLK_1             = 49
    | SDLK_2             = 50
    | SDLK_3             = 51
    | SDLK_4             = 52
    | SDLK_5             = 53
    | SDLK_6             = 54
    | SDLK_7             = 55
    | SDLK_8             = 56
    | SDLK_9             = 57
    | SDLK_COLON         = 58
    | SDLK_SEMICOLON     = 59
    | SDLK_LESS          = 60
    | SDLK_EQUALS        = 61
    | SDLK_GREATER       = 62
    | SDLK_QUESTION      = 63
    | SDLK_AT            = 64
    | SDLK_LEFTBRACKET   = 91
    | SDLK_BACKSLASH     = 92
    | SDLK_RIGHTBRACKET  = 93
    | SDLK_CARET         = 94
    | SDLK_UNDERSCORE    = 95
    | SDLK_BACKQUOTE     = 96
    | SDLK_a             = 97
    | SDLK_b             = 98
    | SDLK_c             = 99
    | SDLK_d             = 100
    | SDLK_e             = 101
    | SDLK_f             = 102
    | SDLK_g             = 103
    | SDLK_h             = 104
    | SDLK_i             = 105
    | SDLK_j             = 106
    | SDLK_k             = 107
    | SDLK_l             = 108
    | SDLK_m             = 109
    | SDLK_n             = 110
    | SDLK_o             = 111
    | SDLK_p             = 112
    | SDLK_q             = 113
    | SDLK_r             = 114
    | SDLK_s             = 115
    | SDLK_t             = 116
    | SDLK_u             = 117
    | SDLK_v             = 118
    | SDLK_w             = 119
    | SDLK_x             = 120
    | SDLK_y             = 121
    | SDLK_z             = 122
    | SDLK_DELETE        = 127
    | SDLK_CAPSLOCK      = 1073741881
    | SDLK_F1            = 1073741882
    | SDLK_F2            = 1073741883
    | SDLK_F3            = 1073741884
    | SDLK_F4            = 1073741885
    | SDLK_F5            = 1073741886
    | SDLK_F6            = 1073741887
    | SDLK_F7            = 1073741888
    | SDLK_F8            = 1073741889
    | SDLK_F9            = 1073741890
    | SDLK_F10           = 1073741891
    | SDLK_F11           = 1073741892
    | SDLK_F12           = 1073741893
    | SDLK_INSERT        = 1073741897
    | SDLK_HOME          = 1073741898
    | SDLK_PAGEUP        = 1073741899
    | SDLK_END           = 1073741901
    | SDLK_PAGEDOWN      = 1073741902
    | SDLK_RIGHT         = 1073741903
    | SDLK_LEFT          = 1073741904
    | SDLK_DOWN          = 1073741905
    | SDLK_UP            = 1073741906
    | SDLK_LCTRL         = 1073742048
    | SDLK_LSHIFT        = 1073742049
    | SDLK_LALT          = 1073742050
    | SDLK_LGUI          = 1073742051
    | SDLK_RCTRL         = 1073742052
    | SDLK_RSHIFT        = 1073742053
    | SDLK_RALT          = 1073742054
    | SDLK_RGUI          = 1073742055
