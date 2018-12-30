using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IdleMouse.Models;

namespace IdleMouse.Controls
{
    internal abstract class ControlPointCollection : IEnumerable<ControlPoint>
    {
        private const float CONTROL_POINT_RADIUS = 5.0f;

        protected readonly List<ControlPoint> _ControlPoints = new List<ControlPoint>();
        protected readonly RectangleF _SafeRegion;

        protected ControlPointCollection(RectangleF safeRegion)
        {
            _SafeRegion = safeRegion;
        }

        protected abstract bool AllowAdd { get; }

        public RectangleF SafeRegion => _SafeRegion;

        #region IEnumerable

        public IEnumerator<ControlPoint> GetEnumerator()
        {
            return _ControlPoints.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _ControlPoints.GetEnumerator();
        }

        #endregion

        public virtual ControlPoint SetLocation(ControlPoint cp, float cX, float cY)
        {
            if (cX < _SafeRegion.Left) cX = _SafeRegion.Left;
            if (cX > _SafeRegion.Right) cX = _SafeRegion.Right;
            if (cY < _SafeRegion.Top) cY = _SafeRegion.Top;
            if (cY > _SafeRegion.Bottom) cY = _SafeRegion.Bottom;

            cp.SetLocation(cX - CONTROL_POINT_RADIUS, cY - CONTROL_POINT_RADIUS);
            return cp;
        }

        public PointF GetModelPoint(ControlPoint cp)
        {
            if (!_ControlPoints.Contains(cp)) throw new ArgumentException("ControlPoint is invalid: not in collection");
            return LocationToModel(cp.Center);
        }

        public PointF AddLocationPoint(PointF loc, Cursor cursor)
        {
            var pt = LocationToModel(loc);
            if (AllowAdd)
            {
                var cp = CreateControlPoint(loc, cursor);
                MoveToTop(cp);
            }
            return pt;
        }

        public bool Remove(ControlPoint cp)
        {
            return _ControlPoints.Remove(cp);
        }

        public void MoveToTop(ControlPoint cp)
        {
            var oldZIndex = cp.ZIndex;
            cp.ZIndex = 0;
            foreach (var other in _ControlPoints.Where(c => c.ZIndex < oldZIndex))
            {
                other.ZIndex++;
            }
        }

        public int IndexOf(ControlPoint cp)
        {
            return _ControlPoints.IndexOf(cp);
        }

        public RectangleF GetModelBounds()
        {
            var x1 = _ControlPoints.Min(cp => cp.Center.X);
            var y1 = _ControlPoints.Min(cp => cp.Center.Y);
            var x2 = _ControlPoints.Max(cp => cp.Center.X);
            var y2 = _ControlPoints.Max(cp => cp.Center.Y);
            var pt1 = LocationToModel(new PointF(x1, y1));
            var pt2 = LocationToModel(new PointF(x2, y2));
            return new RectangleF(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y);
        }

        protected PointF LocationToModel(PointF loc)
        {
            var x = (loc.X - _SafeRegion.X) / _SafeRegion.Width;
            var y = (loc.Y - _SafeRegion.Y) / _SafeRegion.Height;
            if (x < 0) x = 0;
            if (x > 1) x = 1;
            if (y < 0) y = 0;
            if (y > 1) y = 1;
            return new PointF(x, y);
        }

        protected ControlPoint CreateControlPoint(PointF center, Cursor cursor)
        {
            var rect = new RectangleF(center.X - CONTROL_POINT_RADIUS, center.Y - CONTROL_POINT_RADIUS, CONTROL_POINT_RADIUS * 2, CONTROL_POINT_RADIUS * 2);
            var cp = new ControlPoint(_ControlPoints.Count, rect, cursor);
            _ControlPoints.Add(cp);
            return cp;
        }
    }

    internal class FreeControlPointCollection : ControlPointCollection
    {
        public FreeControlPointCollection(RectangleF safeRegion, AnimationPath path) : base(safeRegion)
        {
            var c = path.PointList.Count;
            for (int i = 0; i < c; i++)
            {
                CreateControlPoint(new PointF(_SafeRegion.X + path.PointList[i].X * _SafeRegion.Width, _SafeRegion.Y + path.PointList[i].Y * _SafeRegion.Height), Cursors.SizeAll);
            }
        }

        protected override bool AllowAdd => true;
    }

    internal class RegionControlPointCollection : ControlPointCollection
    {
        // Represents a bounding box for the center points of the each control point in the collection
        private RectangleF _BoundRect;

        public RegionControlPointCollection(RectangleF safeRegion, AnimationEllipse ellipse) : base(safeRegion)
        {
            var x = _SafeRegion.X + (ellipse.CX - ellipse.RX) * _SafeRegion.Width;
            var y = _SafeRegion.Y + (ellipse.CY - ellipse.RY) * _SafeRegion.Height;
            var width = ellipse.RX * 2 * _SafeRegion.Width;
            var height = ellipse.RY * 2 * _SafeRegion.Height;

            _BoundRect = new RectangleF(x, y, width, height);

            CreateControlPoint(new PointF(x, y), Cursors.SizeNWSE);
            CreateControlPoint(new PointF(x + width / 2, y), Cursors.SizeNS);
            CreateControlPoint(new PointF(x + width, y), Cursors.SizeNESW);
            CreateControlPoint(new PointF(x, y + height / 2), Cursors.SizeWE);
            CreateControlPoint(new PointF(x + width, y + height / 2), Cursors.SizeWE);
            CreateControlPoint(new PointF(x, y + height), Cursors.SizeNESW);
            CreateControlPoint(new PointF(x + width / 2, y + height), Cursors.SizeNS);
            CreateControlPoint(new PointF(x + width, y + height), Cursors.SizeNWSE);
        }

        // Bound Rect control point IDs:
        // 0  1  2
        // 3     4
        // 5  6  7
        public override ControlPoint SetLocation(ControlPoint cp, float cX, float cY)
        {
            if (cX < _SafeRegion.Left) cX = _SafeRegion.Left;
            if (cX > _SafeRegion.Right) cX = _SafeRegion.Right;
            if (cY < _SafeRegion.Top) cY = _SafeRegion.Top;
            if (cY > _SafeRegion.Bottom) cY = _SafeRegion.Bottom;

            var id = _ControlPoints.IndexOf(cp);

            // Compute new bound rect
            var top = _BoundRect.Top;
            var left = _BoundRect.Left;
            var bottom = _BoundRect.Bottom;
            var right = _BoundRect.Right;
            if (id == 0 || id == 1 || id == 2) top = cY;
            if (id == 0 || id == 3 || id == 5) left = cX;
            if (id == 2 || id == 4 || id == 7) right = cX;
            if (id == 5 || id == 6 || id == 7) bottom = cY;
            if (top > bottom)
            {
                Swap(ref top, ref bottom);
                switch (id)
                {
                    case 0:
                        id = 5;
                        break;
                    case 1:
                        id = 6;
                        break;
                    case 2:
                        id = 7;
                        break;
                    case 5:
                        id = 0;
                        break;
                    case 6:
                        id = 1;
                        break;
                    case 7:
                        id = 2;
                        break;
                }
            }
            if (left > right)
            {
                Swap(ref left, ref right);
                switch (id)
                {
                    case 0:
                        id = 2;
                        break;
                    case 3:
                        id = 4;
                        break;
                    case 5:
                        id = 7;
                        break;
                    case 2:
                        id = 0;
                        break;
                    case 4:
                        id = 3;
                        break;
                    case 7:
                        id = 5;
                        break;
                }
            }
            _BoundRect = new RectangleF(left, top, right - left, bottom - top);

            // Recompute all control points
            cX = _BoundRect.X;
            cY = _BoundRect.Y;
            var width = _BoundRect.Width;
            var height = _BoundRect.Height;
            base.SetLocation(_ControlPoints[0], cX, cY);
            base.SetLocation(_ControlPoints[1], cX + width / 2, cY);
            base.SetLocation(_ControlPoints[2], cX + width, cY);
            base.SetLocation(_ControlPoints[3], cX, cY + height / 2);
            base.SetLocation(_ControlPoints[4], cX + width, cY + height / 2);
            base.SetLocation(_ControlPoints[5], cX, cY + height);
            base.SetLocation(_ControlPoints[6], cX + width / 2, cY + height);
            base.SetLocation(_ControlPoints[7], cX + width, cY + height);

            return _ControlPoints[id];
        }

        protected override bool AllowAdd => false;

        private void Swap(ref float a, ref float b)
        {
            float t = a;
            a = b;
            b = t;
        }
    }

    internal class ControlPoint
    {
        public ControlPoint(int zIndex, RectangleF region, Cursor cursor)
        {
            ZIndex = zIndex;
            Region = region;
            Cursor = cursor;
        }

        public Cursor Cursor { get; }
        public int ZIndex { get; set; }

        public RectangleF Region { get; private set; }

        public PointF Center => new PointF(Region.X + Region.Width / 2, Region.Y + Region.Height / 2);

        public bool HitTest(Point p)
        {
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(Region);
                return gp.IsVisible(p);
            }
        }

        public void SetLocation(float x, float y)
        {
            Region = new RectangleF(x, y, Region.Width, Region.Height);
        }
    }
}
