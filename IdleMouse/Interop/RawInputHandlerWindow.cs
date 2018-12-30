using IdleMouse.Interop.IcoCurAni;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using SDPoint = System.Drawing.Point;
using Settings = IdleMouse.Properties.Settings;

namespace IdleMouse.Interop
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    internal class RawInputHandlerWindow : IDisposable
    {
        private const string CLASS_NAME = "IdleMouse.RawInputHandlerWindowClass";

        private int _LastX = -1;
        private int _LastY = -1;
        private int _NewX;
        private int _NewY;
        private int _BoundsW;
        private int _BoundsH;

        private bool _CurrentScreensaverDisabled;
        private IntPtr _CurrentCursorHandle;
        private bool _CursorCaptured;

        private WndProc _CursorContainerWndProc;

        private IntPtr _Handle;

        #region Events

        public event EventHandler<MouseEventArgs> MouseMove;
        
        protected void OnMouseMove(int x, int y)
        {
            MouseMove?.Invoke(this, new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
        }

        public event EventHandler<MouseEventArgs> MouseUp;

        protected void OnMouseUp(MouseButtons buttons)
        {
            MouseUp?.Invoke(this, new MouseEventArgs(buttons, 0, 0, 0, 0));
        }

        public event EventHandler<MouseEventArgs> MouseDown;

        protected void OnMouseDown(MouseButtons buttons)
        {
            MouseDown?.Invoke(this, new MouseEventArgs(buttons, 0, 0, 0, 0));
        }

        public event EventHandler<MouseEventArgs> MouseWheel;

        protected void OnMouseWheel(int delta)
        {
            MouseWheel?.Invoke(this, new MouseEventArgs(MouseButtons.None, 0, 0, 0, delta));
        }

        public event EventHandler ShowConfig;

        protected void OnShowConfig()
        {
            ShowConfig?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        public RawInputHandlerWindow()
        {
            _CursorContainerWndProc = _WndProc;
            
            var wndClass = new WndClassEx();
            wndClass.cbSize = (uint)Marshal.SizeOf<WndClassEx>();
            wndClass.lpszClassName = CLASS_NAME;
            wndClass.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(_CursorContainerWndProc);
            wndClass.hCursor = IntPtr.Zero;
            ushort atom = Win32.RegisterClassExA(ref wndClass);
            if (atom == 0) throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to register window class");

            _Handle = Win32.CreateWindowExW(Win32.WS_EX_NOACTIVATE, new IntPtr(atom), null, /* Style: */ Win32.WS_POPUP, 0x7FFFFFFF, 0x7FFFFFFF, 0, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            if (_Handle == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to create window");

            GetCursorPos();

            RawInputDevice[] rawDevices =
            {
                new RawInputDevice
                {
                    UsagePage = 0x01, // From USB spec for HID mouse
                    Usage = 0x02,     // From USB spec for HID mouse
                    Flags = Win32.RIDEV_INPUTSINK,
                    Target = _Handle
                }
            };

            if (!Win32.RegisterRawInputDevices(rawDevices, 1, (uint)Marshal.SizeOf<RawInputDevice>()))
            {
                int err = Marshal.GetLastWin32Error();
                throw new Win32Exception(err, "Failed to register raw input device listener");
            }

            Settings.Default.PropertyChanged += Settings_PropertyChanged;
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.DisableScreensaver))
            {
                if (_CurrentScreensaverDisabled != Settings.Default.DisableScreensaver)
                {
                    _CurrentScreensaverDisabled = Settings.Default.DisableScreensaver;
                    Win32.PostMessageA(_Handle, Win32.WM_APP_SETDISABLESCREENSAVER, UIntPtr.Zero, new IntPtr(_CurrentScreensaverDisabled ? 1 : 0));
                }
            }
        }

        public void MoveCursor(RectangleF bounds, PointF point, CursorModel cursor)
        {
            _BoundsW = (int)bounds.Width;
            _BoundsH = (int)bounds.Height;
            _NewX = (int)point.X;
            _NewY = (int)point.Y;
            if (cursor != null)
            {
                cursor.CreateHandle();
                _CurrentCursorHandle = cursor.Handle;
            }
            else
            {
                _CurrentCursorHandle = IntPtr.Zero;
            }
            Win32.PostMessageA(_Handle, Win32.WM_APP_SETCURSORPOS, UIntPtr.Zero, IntPtr.Zero);
        }

        public void ResetCursor()
        {
            _CurrentCursorHandle = IntPtr.Zero;
            Win32.PostMessageA(_Handle, Win32.WM_APP_RELEASECURSOR, UIntPtr.Zero, IntPtr.Zero);
        }

        public SDPoint GetCurrentPosition()
        {
            return new SDPoint(_LastX, _LastY);
        }

        private IntPtr _WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                case Win32.WM_INPUT:
                    HandleRawInput(lParam);
                    break;
                case Win32.WM_SETCURSOR:
                    if (_CurrentCursorHandle != IntPtr.Zero)
                    {
                        Win32.SetCursor(_CurrentCursorHandle);
                        return new IntPtr(1);
                    }
                    break;
                case Win32.WM_APP_SETCURSORPOS:
                    if (!_CursorCaptured)
                    {
                        Win32.SetWindowPos(_Handle, IntPtr.Zero, _LastX - 32, _LastY - 32, _BoundsW + 64, _BoundsH + 64, Win32.SWP_SHOWWINDOW);
                        _CursorCaptured = true;
                    }
                    Win32.SetCursorPos(_LastX + _NewX, _LastY + _NewY);
                    break;
                case Win32.WM_APP_SETDISABLESCREENSAVER:
                    Win32.SetThreadExecutionState(lParam.ToInt32() == 1 ? EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED : 0);
                    break;
                case Win32.WM_APP_RELEASECURSOR:
                    if (_CursorCaptured)
                    {
                        Win32.SetWindowPos(_Handle, IntPtr.Zero, 0x7FFFFFFF, 0x7FFFFFFF, 0, 0, Win32.SWP_HIDEWINDOW);
                        _CursorCaptured = false;
                    }
                    break;
            }
            if (msg == Win32.WM_APP_SHOWCONFIG)
            {
                OnShowConfig();
            }
            return Win32.DefWindowProcW(hWnd, msg, wParam, lParam);
        }

        private void HandleRawInput(IntPtr ptr)
        {
            uint size = 0;
            uint hSize = (uint)Marshal.SizeOf<RawInputHeader>();
            Win32.GetRawInputData(ptr, Win32.RID_INPUT, IntPtr.Zero, ref size, hSize);
            RawInput input;
            int receivedBytes = Win32.GetRawInputData(ptr, Win32.RID_INPUT, out input, ref size, hSize);
            if (receivedBytes == size && input.Header.Type == Win32.RIM_TYPEMOUSE)
            {
                if (CheckFlags(input.Mouse.Flags, Win32.MOUSE_MOVE_RELATIVE) && (input.Mouse.LastX != 0 || input.Mouse.LastY != 0))
                {
                    if (GetCursorPos()) OnMouseMove(_LastX, _LastY);
                }

                var buttonsDown = MouseButtons.None;
                var buttonsUp = MouseButtons.None;
                if (CheckFlags(input.Mouse.Buttons.ButtonFlags, Win32.RI_MOUSE_BUTTON_1_DOWN)) buttonsDown |= MouseButtons.Left;
                if (CheckFlags(input.Mouse.Buttons.ButtonFlags, Win32.RI_MOUSE_BUTTON_1_UP))   buttonsUp |= MouseButtons.Left;
                if (CheckFlags(input.Mouse.Buttons.ButtonFlags, Win32.RI_MOUSE_BUTTON_2_DOWN)) buttonsDown |= MouseButtons.Right;
                if (CheckFlags(input.Mouse.Buttons.ButtonFlags, Win32.RI_MOUSE_BUTTON_2_UP))   buttonsUp |= MouseButtons.Right;
                if (CheckFlags(input.Mouse.Buttons.ButtonFlags, Win32.RI_MOUSE_BUTTON_3_DOWN)) buttonsDown |= MouseButtons.Middle;
                if (CheckFlags(input.Mouse.Buttons.ButtonFlags, Win32.RI_MOUSE_BUTTON_3_UP))   buttonsUp |= MouseButtons.Middle;
                if (CheckFlags(input.Mouse.Buttons.ButtonFlags, Win32.RI_MOUSE_BUTTON_4_DOWN)) buttonsDown |= MouseButtons.XButton1;
                if (CheckFlags(input.Mouse.Buttons.ButtonFlags, Win32.RI_MOUSE_BUTTON_4_UP))   buttonsUp |= MouseButtons.XButton1;
                if (CheckFlags(input.Mouse.Buttons.ButtonFlags, Win32.RI_MOUSE_BUTTON_5_DOWN)) buttonsDown |= MouseButtons.XButton2;
                if (CheckFlags(input.Mouse.Buttons.ButtonFlags, Win32.RI_MOUSE_BUTTON_5_UP))   buttonsUp |= MouseButtons.XButton2;
                if (buttonsDown != MouseButtons.None) OnMouseDown(buttonsDown);
                if (buttonsUp != MouseButtons.None) OnMouseUp(buttonsUp);

                if (CheckFlags(input.Mouse.Buttons.ButtonFlags, Win32.RI_MOUSE_WHEEL)) OnMouseWheel(input.Mouse.Buttons.ButtonData);
            }
        }

        private bool GetCursorPos()
        {
            if (!Win32.GetCursorPos(out Point pt))
            {
                int err = Marshal.GetLastWin32Error();
                throw new Win32Exception(err, "Failed to get cursor position.");
            }
            if (_LastX != pt.X || _LastY != pt.Y)
            {
                _LastX = pt.X;
                _LastY = pt.Y;
                return true;
            }
            return false;
        }

        private bool CheckFlags(ushort flags, ushort mask) => (flags & mask) == mask;
        private bool CheckFlags(uint flags, uint mask) => (flags & mask) == mask;

        #region IDisposable impl

        private bool _Disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed) return;

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            if (_Handle != IntPtr.Zero)
            {
                Win32.DestroyWindow(_Handle);
                _Handle = IntPtr.Zero;
            }

            _Disposed = true;
        }

        ~RawInputHandlerWindow()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
