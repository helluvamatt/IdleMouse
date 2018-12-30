using IdleMouse.Interop.IcoCurAni;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IdleMouse.Controls
{
    internal class AnimatedCursorPreview : Control
    {
        private readonly AnimationHandler _AnimationHandler;

        public AnimatedCursorPreview()
        {
            _AnimationHandler = new AnimationHandler(OnAnimationTick);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public CursorModel CursorIcon
        {
            get => _AnimationHandler.Cursor;
            set
            {
                _AnimationHandler.Cursor = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var img = _AnimationHandler.GetCurrentFrame();
            if (img != null) e.Graphics.DrawImage(img, new Point((Width - img.Width) / 2, (Height - img.Height) / 2));
        }

        protected override void DestroyHandle()
        {
            _AnimationHandler.Dispose();
            base.DestroyHandle();
        }

        private void OnAnimationTick(bool updated)
        {
            if (updated && IsHandleCreated) Invoke(new Action(Invalidate));
        }

    }
}
