using System;
using System.Runtime.InteropServices;

namespace IdleMouse.Interop
{
    internal delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct WndClassEx
    {
        public uint cbSize;
        public uint style;
        public IntPtr lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        [MarshalAs(UnmanagedType.LPStr)] public string lpszMenuName;
        [MarshalAs(UnmanagedType.LPStr)] public string lpszClassName;
        public IntPtr hIconSm;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct RawInputDevice
    {
        public ushort UsagePage;
        public ushort Usage;
        public uint Flags;
        public IntPtr Target;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct RawKeyboard
    {
        public ushort MakeCode;
        public ushort Flags;
        public ushort Reserved;
        public ushort VKey;
        public uint Message;
        public uint ExtraInformation;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    internal struct RawMouse
    {
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(0)]
        public ushort Flags;
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(4)]
        public uint ButtonsRaw;
        [FieldOffset(4)]
        public RawMouseButtons Buttons;
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(8)]
        public uint RawButtons;
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(12)]
        public int LastX;
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(16)]
        public int LastY;
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(20)]
        public uint ExtraInformation;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    internal struct RawMouseButtons
    {
        [FieldOffset(0)]
        public ushort ButtonFlags;
        [FieldOffset(2)]
        public ushort ButtonData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct RawHid
    {
        public uint SizeHid;
        public uint Count;
        public IntPtr RawData;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    internal struct RawInput
    {
        [FieldOffset(0)]
        public RawInputHeader Header;
        [FieldOffset(16)]
        public RawMouse Mouse;
        [FieldOffset(16)]
        public RawKeyboard Keyboard;
        [FieldOffset(16)]
        public RawHid Hid;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct RawInputHeader
    {
        public uint Type;
        public uint Size;
        public IntPtr hDevice;
        public IntPtr wParam;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct RawInputDeviceInfo
    {
        public uint Size;
        public uint Type;
        public RawInputDeviceInfoData Data;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    internal struct RawInputDeviceInfoData
    {
        [FieldOffset(0)]
        public RawInputDeviceInfoDataMouse Mouse;
        [FieldOffset(0)]
        public RawInputDeviceInfoDataKeyboard Keyboard;
        [FieldOffset(0)]
        public RawInputDeviceInfoDataHid Hid;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct RawInputDeviceInfoDataMouse
    {
        public uint Id;
        public uint NumberOfButtons;
        public uint SampleRate;
        public bool HasHorizontalWheel;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct RawInputDeviceInfoDataKeyboard
    {
        public uint Type;
        public uint SubType;
        public uint KeyboardMode;
        public uint NumberOfFunctionKeys;
        public uint NumberOfIndicators;
        public uint NumberOfKeysTotal;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct RawInputDeviceInfoDataHid
    {
        public uint VendorId;
        public uint ProductId;
        public uint VersionNumber;
        public ushort UsagePage;
        public ushort Usage;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Point
    {
        public int X;
        public int Y;
    }

    [Flags]
    internal enum EXECUTION_STATE : uint
    {
        ES_SYSTEM_REQUIRED = 0x00000001,
        ES_DISPLAY_REQUIRED = 0x00000002,
        // Legacy flag, should not be used.
        // ES_USER_PRESENT   = 0x00000004,
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
    }

    [Flags]
    internal enum FlashWinInfoFlags : int
    {
        FLASHW_STOP = 0,
        FLASHW_CAPTION = 0x00000001,
        FLASHW_TRAY = 0x00000002,
        FLASHW_TIMER = 0x00000004,
        FLASHW_TIMERNOFG = 0x0000000C,
        FLASHW_ALL = FLASHW_CAPTION | FLASHW_TRAY
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct FlashWinInfo
    {
        public uint Size;
        public IntPtr Hwnd;
        public FlashWinInfoFlags Flags;
        public uint Count;
        public int Timeout;
    }

    internal static class Win32
    {
        #region Consts

        public const int HWND_BROADCAST = 0xffff;

        public static readonly uint WM_APP_SHOWCONFIG = RegisterWindowMessage("WM_APP_SHOWCONFIG");

        public const int WM_SETCURSOR = 0x20;
        public const int WM_INPUT = 0xFF;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_APP_SETCURSORPOS = 0x8001;
        public const int WM_APP_SETDISABLESCREENSAVER = 0x8002;
        public const int WM_APP_RELEASECURSOR = 0x8003;

        public const uint WS_POPUP = 0x80000000;
        public const uint WS_CLIPCHILDREN = 0x2000000;
        
        public const uint WS_EX_TRANSPARENT = 0x00000020;
        public const uint WS_EX_LAYERED = 0x00080000;
        public const uint WS_EX_NOACTIVATE = 0x08000000;

        public const int SWP_SHOWWINDOW = 0x0040;
        public const int SWP_HIDEWINDOW = 0x0080;

        public const int ERROR_CLASS_ALREADY_EXISTS = 1410;

        public const int RID_HEADER = 0x10000005;
        public const int RID_INPUT = 0x10000003;

        public const uint RIDEV_REMOVE = 0x1;
        public const uint RIDEV_EXCLUDE = 0x10;
        public const uint RIDEV_PAGEONLY = 0x20;
        public const uint RIDEV_NOLEGACY = 0x30;
        public const uint RIDEV_INPUTSINK = 0x100;
        public const uint RIDEV_CAPTUREMOUSE = 0x200;
        public const uint RIDEV_NOHOTKEYS = 0x200;
        public const uint RIDEV_APPKEYS = 0x400;
        public const uint RIDEV_EXINPUTSINK = 0x1000;
        public const uint RIDEV_DEVNOTIFY = 0x2000;

        public const ushort MOUSE_ATTRIBUTES_CHANGED = 0x04;
        public const ushort MOUSE_MOVE_RELATIVE = 0;
        public const ushort MOUSE_MOVE_ABSOLUTE = 1;
        public const ushort MOUSE_VIRTUAL_DESKTOP = 0x02;

        public const uint RIM_TYPEMOUSE = 0;
        public const uint RIM_TYPEKEYBOARD = 1;
        public const uint RIM_TYPEHID = 2;

        public const ushort RI_KEY_MAKE = 0;
        public const ushort RI_KEY_BREAK = 1;
        public const ushort RI_KEY_E0 = 2;
        public const ushort RI_KEY_E1 = 4;

        public const ushort RI_MOUSE_BUTTON_1_DOWN = 0x0001;
        public const ushort RI_MOUSE_BUTTON_1_UP = 0x0002;
        public const ushort RI_MOUSE_BUTTON_2_DOWN = 0x0004;
        public const ushort RI_MOUSE_BUTTON_2_UP = 0x0008;
        public const ushort RI_MOUSE_BUTTON_3_DOWN = 0x0010;
        public const ushort RI_MOUSE_BUTTON_3_UP = 0x0020;
        public const ushort RI_MOUSE_BUTTON_4_DOWN = 0x0040;
        public const ushort RI_MOUSE_BUTTON_4_UP = 0x0080;
        public const ushort RI_MOUSE_BUTTON_5_DOWN = 0x100;
        public const ushort RI_MOUSE_BUTTON_5_UP = 0x0200;
        public const ushort RI_MOUSE_WHEEL = 0x0400;

        public const ushort RI_MOUSE_LEFT_BUTTON_DOWN = RI_MOUSE_BUTTON_1_DOWN;
        public const ushort RI_MOUSE_LEFT_BUTTON_UP = RI_MOUSE_BUTTON_1_UP;
        public const ushort RI_MOUSE_MIDDLE_BUTTON_DOWN = RI_MOUSE_BUTTON_3_DOWN;
        public const ushort RI_MOUSE_MIDDLE_BUTTON_UP = RI_MOUSE_BUTTON_3_UP;
        public const ushort RI_MOUSE_RIGHT_BUTTON_DOWN = RI_MOUSE_BUTTON_2_DOWN;
        public const ushort RI_MOUSE_RIGHT_BUTTON_UP = RI_MOUSE_BUTTON_2_UP;

        public const uint RIDI_DEVICENAME = 0x20000007;
        public const uint RIDI_DEVICEINFO = 0x2000000b;
        public const uint RIDI_PREPARSEDDATA = 0x20000005;

        public const uint DI_MASK = 0x0001;
        public const uint DI_IMAGE = 0x0002;
        public const uint DI_COMPAT = 0x0004;
        public const uint DI_DEFAULTSIZE = 0x0008;
        public const uint DI_NOMIRROR = 0x0010;
        public const uint DI_NORMAL = DI_IMAGE | DI_MASK;

        #endregion

        #region Windows

        [DllImport("User32", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool PostMessageA(IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("User32", SetLastError = true)]
        public static extern uint RegisterWindowMessage(string message);

        [DllImport("User32", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern ushort RegisterClassExA(ref WndClassEx lpWndClassEx);

        [DllImport("User32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindowExW(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int X, int Y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("User32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindowExW(uint dwExStyle, IntPtr lpClassName, string lpWindowName, uint dwStyle, int X, int Y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr DefWindowProcW(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", SetLastError = true)]
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("User32", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int width, int height, uint uFlags);

        #endregion

        #region Raw Input

        [DllImport("User32", SetLastError = true)]
        public static extern int GetRawInputData(IntPtr rawInput, uint command, IntPtr data, [In, Out] ref uint size, uint sizeHeader);

        [DllImport("User32", SetLastError = true)]
        public static extern int GetRawInputData(IntPtr rawInput, uint command, [Out] out RawInput data, [In, Out] ref uint size, uint sizeHeader);

        [DllImport("User32", SetLastError = true)]
        public static extern bool RegisterRawInputDevices([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] RawInputDevice[] devices, uint numDevices, uint size);

        [DllImport("User32", SetLastError = true)]
        public static extern int GetRawInputDeviceInfoA(IntPtr hDevice, uint uiCommand, IntPtr pData, [Out] out uint pcbSize);

        #endregion

        #region Cursors

        [DllImport("User32", SetLastError = true)]
        public static extern bool GetCursorPos([Out] out Point point);

        [DllImport("User32", SetLastError = true)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr SetCursor(IntPtr hCursor);

        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr LoadCursorFromFileW([MarshalAs(UnmanagedType.LPTStr)] string path);

        [DllImport("User32", SetLastError = true)]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("User32", SetLastError = true)]
        public static extern bool ReleaseCapture();

        #endregion

        #region Window flash

        [DllImport("User32", SetLastError = true)]
        public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

        [DllImport("User32", SetLastError = true)]
        public static extern bool FlashWindowEx(ref FlashWinInfo info);

        public static void FlashWindow(IntPtr handle, FlashWinInfoFlags flags, uint count, int timeout)
        {
            var flash = new FlashWinInfo
            {
                Size = (uint)Marshal.SizeOf<FlashWinInfo>(),
                Hwnd = handle,
                Flags = flags,
                Count = count,
                Timeout = timeout
            };
            FlashWindowEx(ref flash);
        }

        #endregion

        #region TES

        [DllImport("Kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        #endregion

    }
}
