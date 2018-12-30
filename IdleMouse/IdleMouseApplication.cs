using IdleMouse.Controls;
using IdleMouse.Interop;
using IdleMouse.Models;
using IdleMouse.Util;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using R = IdleMouse.Properties.Resources;
using Settings = IdleMouse.Properties.Settings;

namespace IdleMouse
{
    internal class IdleMouseApplication : ApplicationContext
    {
        private readonly NotifyIcon _TrayIcon;
        private readonly IdleMovementManager _MovementManager;
        private readonly CursorManager _CursorManager;
        private readonly RawInputHandlerWindow _RawHandler;

        private readonly System.Threading.Timer _IdleTimer;
        private bool _SendWindowsEvents = false;

        public IdleMouseApplication(bool showMainForm, IdleMovementManager movementManager, CursorManager cursorManager)
        {
            _MovementManager = movementManager;
            _CursorManager = cursorManager;
            _IdleTimer = new System.Threading.Timer(IdleTimer_Callback, null, -1, -1);
            Settings.Default.PropertyChanged += Settings_PropertyChanged;

            if (Settings.Default.IdleTimeout > 3600) Settings.Default.IdleTimeout = 3600;
            if (Settings.Default.IdleTimeout < 0) Settings.Default.IdleTimeout = 0;

            _TrayIcon = new NotifyIcon();
            _TrayIcon.Icon = R.appicon;
            _TrayIcon.Text = R.AppTitle;
            _TrayIcon.DoubleClick += OnShowConfig;
            _TrayIcon.ContextMenuStrip = new ContextMenuStrip();
            _TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem(R.Configure, null, OnShowConfig));
            _TrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            _TrayIcon.ContextMenuStrip.Items.Add(CreateEnabledCheckedMenuItem());
            _TrayIcon.ContextMenuStrip.Items.Add(CreateDisabledScreenSaverCheckedMenuItem());
            _TrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            _TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem(R.Exit, null, (sender, e) => ExitThread()));
            _TrayIcon.ContextMenuStrip.Items[0].Font = new System.Drawing.Font(_TrayIcon.ContextMenuStrip.Items[0].Font, System.Drawing.FontStyle.Bold);
            _TrayIcon.Visible = true;

            _RawHandler = new RawInputHandlerWindow();
            _RawHandler.MouseDown += RawHandler_MouseEvent;
            _RawHandler.MouseUp += RawHandler_MouseEvent;
            _RawHandler.MouseWheel += RawHandler_MouseEvent;
            _RawHandler.MouseMove += RawHandler_MouseMove;
            _RawHandler.ShowConfig += OnShowConfig;

            _MovementManager.Move += MovementManager_Move;
            _MovementManager.Start();

            if (showMainForm)
            {
                OnShowConfig(this, EventArgs.Empty);
            }
        }

        private ToolStripMenuItem CreateEnabledCheckedMenuItem()
        {
            var item = new BindableToolStripMenuItem(R.Enabled);
            item.CheckOnClick = true;
            item.DataBindings.Add(new Binding(nameof(item.Checked), Settings.Default, nameof(Settings.IdleEnabled), false, DataSourceUpdateMode.OnPropertyChanged));
            return item;
        }

        private ToolStripMenuItem CreateDisabledScreenSaverCheckedMenuItem()
        {
            var item = new BindableToolStripMenuItem(R.DisableScreensaver);
            item.CheckOnClick = true;
            item.DataBindings.Add(new Binding(nameof(item.Checked), Settings.Default, nameof(Settings.DisableScreensaver), false, DataSourceUpdateMode.OnPropertyChanged));
            return item;
        }

        #region Event handlers

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Settings.Default.Save();

            if (e.PropertyName == nameof(Settings.IdleEnabled) || e.PropertyName == nameof(Settings.IdleTimeout))
            {
                _IdleTimer.Change(Settings.Default.IdleEnabled && Settings.Default.IdleTimeout > 0 ? TimeSpan.FromSeconds(Settings.Default.IdleTimeout) : System.Threading.Timeout.InfiniteTimeSpan, System.Threading.Timeout.InfiniteTimeSpan);
            }
        }

        private void OnShowConfig(object sender, EventArgs e)
        {
            bool created = false;
            if (MainForm == null)
            {
                MainForm = new frmMain(_MovementManager, _CursorManager);
                MainForm.FormClosing += MainForm_FormClosing;
                MainForm.Resize += MainForm_Resize;
                created = true;
            }

            MainForm.Show();
            MainForm.Activate();
            (MainForm as frmMain)?.HandleMouseMove(_RawHandler.GetCurrentPosition());
                        
            if (MainForm.WindowState == FormWindowState.Minimized)
            {
                MainForm.WindowState = FormWindowState.Normal;
            }
            else if (!created)
            {
                MainForm.BringToFront(true);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == MainForm.WindowState)
            {
                _TrayIcon.ShowBalloonTip(500, R.AppTitle, R.MinimizedToTray, ToolTipIcon.Info);
                MainForm.Hide();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                _TrayIcon.ShowBalloonTip(500, R.AppTitle, R.MinimizedToTray, ToolTipIcon.Info);
                MainForm.Hide();
                e.Cancel = true;
            }
        }

        private void MovementManager_Move(object sender, MoveEventArgs e)
        {
            if (_SendWindowsEvents)
            {
                _RawHandler.MoveCursor(e.Bounds, e.Point, _CursorManager[_CursorManager.CurrentIndex]);
            }
        }

        private void IdleTimer_Callback(object state)
        {
            _SendWindowsEvents = true;
            var frm = MainForm as frmMain;
            if (frm != null && !frm.IsDisposed && frm.IsHandleCreated)
            {
                frm.SetIdle(true);
            }
        }

        private void RawHandler_MouseEvent(object sender, MouseEventArgs e)
        {
            _IdleTimer.Change(Settings.Default.IdleEnabled && Settings.Default.IdleTimeout > 0 ? TimeSpan.FromSeconds(Settings.Default.IdleTimeout) : System.Threading.Timeout.InfiniteTimeSpan, System.Threading.Timeout.InfiniteTimeSpan);
            if (_SendWindowsEvents)
            {
                _SendWindowsEvents = false;
                _RawHandler.ResetCursor();
                var frm = MainForm as frmMain;
                if (frm != null && !frm.IsDisposed && frm.IsHandleCreated)
                {
                    frm.SetIdle(false);
                }
            }
        }

        private void RawHandler_MouseMove(object sender, MouseEventArgs e)
        {
            RawHandler_MouseEvent(sender, e);
            var frm = MainForm as frmMain;
            if (frm != null && !frm.IsDisposed && frm.IsHandleCreated)
            {
                frm.HandleMouseMove(e.Location);
            }
        }

        #endregion

        #region ApplicationContext overrides

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            // Don't call base here, we only want to exit explicitly
            MainForm = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (_MovementManager != null) _MovementManager.Stop();
            if (_TrayIcon != null) _TrayIcon.Dispose();
            if (MainForm != null) MainForm.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}
