using IdleMouse.Controls;
using IdleMouse.Interop.IcoCurAni;
using IdleMouse.Models;
using IdleMouse.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    internal partial class frmCursorManager : Form
    {
        private readonly CursorManager _CursorManager;
        private readonly MultipleAnimationHandler _AnimationHandler;

        public frmCursorManager(CursorManager cursorManager)
        {
            InitializeComponent();
            _CursorManager = cursorManager;
            _CursorManager.PropertyChanged += CursorManager_PropertyChanged;

            _AnimationHandler = new MultipleAnimationHandler();
            _AnimationHandler.FrameTick += AnimationHandler_FrameTick;

            // Set data source
            _AnimationHandler.DataSource = cursorModelBindingSource.DataSource = _CursorManager.Cursors;

            SetPreview();
        }

        protected override void OnClosed(EventArgs e)
        {
            _CursorManager.PropertyChanged -= CursorManager_PropertyChanged;
            base.OnClosed(e);
        }

        #region Event handlers

        private void CursorManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CursorManager.CurrentIndex))
            {
                SetPreview();
                cursorModelBindingSource.Position = _CursorManager.CurrentIndex;
            }
        }

        private void AnimationHandler_FrameTick(object sender, FrameAnimationEventArgs e)
        {
            if (e.Invalidated && !IsDisposed && IsHandleCreated)
            {
                Invoke(new Action(() =>
                {
                    var rect = cursorsList.GetItemRectangle(e.Index);
                    cursorsList.Invalidate(rect);
                }));
            }
        }

        private void cursorModelBindingSource_PositionChanged(object sender, EventArgs e)
        {
            _CursorManager.SetCurrent(cursorModelBindingSource.Position);
        }

        private void cursorsList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            var item = _CursorManager[e.Index];
            if (item != null)
            {
                e.Graphics.DrawImage(_AnimationHandler.GetCurrentFrameFor(e.Index), e.Bounds.X + 4, e.Bounds.Y + 4);
                var txtSize = e.Graphics.MeasureString(item.Name, e.Font);
                e.Graphics.DrawString(item.Name, e.Font, new SolidBrush(e.ForeColor), e.Bounds.X + e.Bounds.Height, e.Bounds.Y + (e.Bounds.Height - txtSize.Height) / 2);
            }
            else
            {
                var txtSize = e.Graphics.MeasureString(R.None, e.Font);
                e.Graphics.DrawString(R.None, e.Font, new SolidBrush(e.ForeColor), e.Bounds.X + e.Bounds.Height, e.Bounds.Y + (e.Bounds.Height - txtSize.Height) / 2);
            }
            e.DrawFocusRectangle();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    _CursorManager.LoadAndSetCurrent(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, R.ErrorFailedToLoadCursor + ex.Message, R.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _CursorManager.RemoveAt(cursorModelBindingSource.Position);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        private void SetPreview()
        {
            var currentCursor = _CursorManager[_CursorManager.CurrentIndex];
            animatedCursorPreview.CursorIcon = currentCursor;
            if (currentCursor != null)
            {
                lblPreviewName.Text = currentCursor.Name;
                lblPreviewName.Font = Font;

                lblTypeValue.Text = currentCursor.Type;
                lblSizeValue.Text = FormUtils.SizeToString(currentCursor.Size);
                lblHotspotValue.Text = string.Format("{0}, {1}", currentCursor.HotspotX, currentCursor.HotspotY);
                lblFrameCountValue.Text = string.Format("{0}", currentCursor.Frames.Length);
                
                tableLayoutPanelDetails.Visible = true;
            }
            else
            {
                lblPreviewName.Text = R.None;
                lblPreviewName.Font = new Font(Font, FontStyle.Italic);
                tableLayoutPanelDetails.Visible = false;
            }
        }
    }
}
