using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IdleMouse.Controls
{
    internal class MovementPreviewControl : Control
    {
        private string _AnimationName;
        private float _AnimationProgress;
        private RectangleF _Region;
        private PointF _CurrentPoint;
        private Image _Icon;

        public MovementPreviewControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            using (var rawIcon = Properties.Resources.appicon.ToBitmap())
            {
                SetCursor(rawIcon);
            }
        }

        public void SetCursor(Image cursor)
        {
            _Icon = new Bitmap(cursor, 16, 16);
            Invalidate();
        }

        public void HandleFrame(string animationName, float progress, RectangleF region, PointF point)
        {
            _AnimationName = animationName;
            _AnimationProgress = progress;
            _Region = region;
            _CurrentPoint = point;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Scale bounding box to canvas
            var scaledBox = RectangleF.Empty;
            var scale = 1.0f;

            if (!_Region.IsEmpty)
            {
                var vScale = (Height - 33) / _Region.Height;
                var hScale = (Width - 33) / _Region.Width;
                scale = new float[] { vScale, hScale, 1 }.Min();
                var newH = _Region.Height * scale;
                var newW = _Region.Width * scale;
                scaledBox = new RectangleF((Width - newW) / 2, (Height - newH) / 2, newW, newH);
            }

            var loc = new PointF(scaledBox.X + _CurrentPoint.X * scale, scaledBox.Y + _CurrentPoint.Y * scale);

            var p = new Pen(Color.Gray);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            e.Graphics.DrawRectangle(p, scaledBox.X, scaledBox.Y, scaledBox.Width + 16, scaledBox.Height + 16);
            
            e.Graphics.DrawImage(_Icon, loc);

        }
    }
}
