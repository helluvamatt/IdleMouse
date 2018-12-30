using IdleMouse.Interop.IcoCurAni;
using IdleMouse.Models;
using IdleMouse.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using R = IdleMouse.Properties.Resources;

namespace IdleMouse
{
    internal partial class frmMain : Form
    {
        private readonly IdleMovementManager _MovementManager;
        private readonly CursorManager _CursorManager;
        private DateTime _LastIdleStart = DateTime.Now;

        private frmAnimationManager _AnimationManager;

        public frmMain(IdleMovementManager movementManager, CursorManager cursorManager)
        {
            _MovementManager = movementManager;
            _CursorManager = cursorManager;
            InitializeComponent();
            _IdleTimer.Start();
            
            txtTimeoutValue.Value = Properties.Settings.Default.IdleTimeout;
            txtAnimationWidth.Value = Properties.Settings.Default.IdleAnimationWidth;
            txtAnimationHeight.Value = Properties.Settings.Default.IdleAnimationHeight;
            trkAnimationSpeed.Value = Properties.Settings.Default.IdleAnimationSpeed;

            animationBindingSource.DataSource = _MovementManager.Animations;
            animationBindingSource.CurrencyManager.Position = _MovementManager.GetCurrentAnimationIndex();
            animationBindingSource.CurrentChanged += AnimationBindingSource_CurrentChanged;

            _MovementManager.Move += MovementManager_Move;

            _CursorManager.PropertyChanged += CursorManager_PropertyChanged;
        }

        public void HandleMouseMove(Point location)
        {
            TryInvoke(() =>
            {
                locationStatusLabel.Text = $"{location.X}, {location.Y}";
                _LastIdleStart = DateTime.Now;
                _IdleTimer_Tick(this, EventArgs.Empty);
            });
        }

        public void SetIdle(bool idle)
        {
            TryInvoke(() =>
            {
                idleStatusLabel.ToolTipText = idle ? R.Idle : R.NotIdle;
                idleStatusLabel.Image = idle ? R.user_silhouette : R.user;
            });
        }

        #region Event handlers

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _MovementManager.Move -= MovementManager_Move;
            _CursorManager.PropertyChanged -= CursorManager_PropertyChanged;
            base.OnFormClosing(e);
        }

        private void MovementManager_Move(object sender, MoveEventArgs e)
        {
            TryInvoke(() => movementPreview.HandleFrame(e.AnimationName, e.AnimationProgress, e.Bounds, e.Point));
        }

        private void CursorManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CursorManager.CurrentIndex))
            {
                var currentCursor = _CursorManager[_CursorManager.CurrentIndex];
                btnCursor.CursorIcon = currentCursor;
                btnCursor.Text = currentCursor == null ? R.None : string.Empty;
            }
        }

        private void AnimationBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IdleAnimation = animationBindingSource.Position;
        }

        private void txtTimeoutValue_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IdleTimeout = Convert.ToInt64(txtTimeoutValue.Value);
        }

        private void txtAnimationWidth_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IdleAnimationWidth = Convert.ToUInt16(txtAnimationWidth.Value);
        }

        private void txtAnimationHeight_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IdleAnimationHeight = Convert.ToUInt16(txtAnimationHeight.Value);
        }

        private void trkAnimationSpeed_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IdleAnimationSpeed = Convert.ToUInt16(trkAnimationSpeed.Value);
        }

        private void _IdleTimer_Tick(object sender, EventArgs e)
        {
            var idleTime = DateTime.Now - _LastIdleStart;
            idleTimeStatusLabel.Text = $"{(long)Math.Floor(idleTime.TotalHours)}:{idleTime.Minutes.ToString("00")}:{idleTime.Seconds.ToString("00")}";
        }

        private void btnCursor_Click(object sender, EventArgs e)
        {
            var dlg = new frmCursorManager(_CursorManager);
            dlg.Show(this);
        }

        private void btnManageAnimations_Click(object sender, EventArgs e)
        {
            bool created = false;
            if (_AnimationManager == null)
            {
                _AnimationManager = new frmAnimationManager(_MovementManager);
                _AnimationManager.FormClosed += (s, args) => _AnimationManager = null;
                created = true;
            }

            _AnimationManager.Show();
            _AnimationManager.Activate();

            if (_AnimationManager.WindowState == FormWindowState.Minimized)
            {
                _AnimationManager.WindowState = FormWindowState.Normal;
            }
            else if (!created)
            {
                _AnimationManager.BringToFront(true);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        private void TryInvoke(Action action)
        {
            if (IsDisposed || !IsHandleCreated) return;
            try
            {
                Invoke(action);
            }
            catch (ObjectDisposedException) { } // Ignore
        }
    }
}
