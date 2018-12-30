using IdleMouse.Models;
using IdleMouse.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace IdleMouse.Controls
{
    internal class AnimationEditorControl : Control, INotifyPropertyChanged
    {
        private static readonly TimeSpan DEFAULT_FRAME_DURATION = TimeSpan.FromMilliseconds(25);

        private Animation _Animation;
        private Image _Icon;
        private Image _PreviewIcon;

        private readonly Thread _AnimationThread;
        private readonly object _LockObject = new { };
        private TimeSpan _CurrentDuration;
        private RectangleF _CurrentBounds;
        private PointF _CurrentPoint;

        public AnimationEditorControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            using (var rawIcon = Resources.appicon.ToBitmap())
            {
                _Icon = new Bitmap(16, 16);
                using (var iconGraphics = Graphics.FromImage(_Icon))
                {
                    iconGraphics.CompositingQuality = CompositingQuality.HighQuality;
                    float[][] matrixItems ={
                       new float[] {1, 0, 0, 0, 0},
                       new float[] {0, 1, 0, 0, 0},
                       new float[] {0, 0, 1, 0, 0},
                       new float[] {0, 0, 0, 0.25f, 0},
                       new float[] {0, 0, 0, 0, 1}};
                    var colorMatrix = new ColorMatrix(matrixItems);
                    var imageAtt = new ImageAttributes();
                    imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    iconGraphics.DrawImage(rawIcon, new Rectangle(0, 0, _Icon.Width, _Icon.Height), 0.0f, 0.0f, rawIcon.Width, rawIcon.Height, GraphicsUnit.Pixel,imageAtt);
                }
                _PreviewIcon = new Bitmap(rawIcon, 16, 16);
            }

            _CurrentBounds = new RectangleF(0, 0, Settings.Default.IdleAnimationWidth, Settings.Default.IdleAnimationHeight);
            _CurrentDuration = IdleMovementManager.ComputeAnimationDuration(null, _CurrentBounds, Settings.Default.IdleAnimationSpeed);
            Settings.Default.PropertyChanged += Settings_PropertyChanged;

            _AnimationThread = new Thread(AnimationThread);
            _AnimationThread.Name = "Animation Editor Preview Thread";
            _AnimationThread.IsBackground = true;
            _AnimationThread.Start();
        }

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        public Animation Animation
        {
            get
            {
                return _Animation;
            }
            set
            {
                if (_Animation != value)
                {
                    lock (_LockObject)
                    {
                        _Animation = value;
                        _CurrentDuration = IdleMovementManager.ComputeAnimationDuration(_Animation, _CurrentBounds, PreviewSpeed);
                    }
                    OnPropertyChanged(nameof(Animation));
                }
                OnPropertyChanged(nameof(IsAllowPointEditing));
                
                Invalidate();
            }
        }

        private AnimationEditorMode _Mode;
        public AnimationEditorMode Mode
        {
            get => _Mode;
            set
            {
                if (_Mode != value)
                {
                    lock (_LockObject) _Mode = value;
                    OnPropertyChanged(nameof(Mode));
                    if (_Mode != AnimationEditorMode.Preview) Invalidate();
                }
            }
        }

        public bool IsAllowPointEditing
        {
            get => _Animation != null && _Animation.ItemType == typeof(AnimationPath);
        }

        private ushort _PreviewSpeed;
        public ushort PreviewSpeed
        {
            get => _PreviewSpeed;
            set
            {
                if (_PreviewSpeed != value)
                {
                    _PreviewSpeed = value;
                    lock (_LockObject)
                    {
                        _CurrentDuration = IdleMovementManager.ComputeAnimationDuration(_Animation, _CurrentBounds, _PreviewSpeed);
                    }
                }
            }
        }

        #endregion

        #region Event handlers

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.IdleAnimationWidth) || e.PropertyName == nameof(Settings.IdleAnimationHeight))
            {
                lock (_LockObject)
                {
                    _CurrentBounds = new RectangleF(0, 0, Settings.Default.IdleAnimationWidth, Settings.Default.IdleAnimationHeight);
                    _CurrentDuration = IdleMovementManager.ComputeAnimationDuration(_Animation, _CurrentBounds, PreviewSpeed);
                }
                Invalidate();
            }
        }

        #endregion

        #region Mouse handling

        private ControlPoint _DragCP = null;
        private RectangleF _SafeRegion;
        private PointF _DragCPOffset;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (Animation == null) return;
            if (Mode == AnimationEditorMode.Select)
            {
                foreach (var cp in _ControlPoints.OrderBy(cp => cp.ZIndex))
                {
                    if (cp.HitTest(e.Location))
                    {
                        _DragCP = cp;
                        _DragCPOffset = new PointF(e.X - _DragCP.Center.X, e.Y - _DragCP.Center.Y);
                        break;
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Animation == null) return;
            var cursor = Cursors.Default;
            var ellipse = Animation.Item as AnimationEllipse;
            var path = Animation.Item as AnimationPath;
            if (Mode == AnimationEditorMode.Select)
            {
                if (_DragCP != null)
                {
                    float x = e.X - _DragCPOffset.X;
                    float y = e.Y - _DragCPOffset.Y;

                    _DragCP = _ControlPoints.SetLocation(_DragCP, x, y);

                    if (ellipse != null)
                    {
                        var region = _ControlPoints.GetModelBounds();

                        // Set Radius
                        ellipse.RX = region.Width / 2;
                        ellipse.RY = region.Height / 2;
                        // Set Center
                        ellipse.CX = region.X + ellipse.RX;
                        ellipse.CY = region.Y + ellipse.RY;
                    }
                    else if (path != null)
                    {
                        var index = _ControlPoints.IndexOf(_DragCP);
                        if (index > -1)
                        {
                            var pt = _ControlPoints.GetModelPoint(_DragCP);
                            path.PointList[index].X = pt.X;
                            path.PointList[index].Y = pt.Y;
                        }
                    }

                    cursor = _DragCP.Cursor; // Don't change the cursor until mouse up
                    Invalidate();
                }
                else if (_ControlPoints != null)
                {
                    foreach (var cp in _ControlPoints.OrderBy(cp => cp.ZIndex))
                    {
                        if (cp.HitTest(e.Location))
                        {
                            cursor = cp.Cursor;
                            break;
                        }
                    }
                }
            }
            else if (Mode == AnimationEditorMode.AddPathPoint && path != null)
            {
                if (_SafeRegion.Contains(e.X, e.Y)) cursor = Cursors.Cross;
            }
            else if (Mode == AnimationEditorMode.RemovePathPoint && path != null)
            {
                foreach (var cp in _ControlPoints)
                {
                    if (cp.HitTest(e.Location))
                    {
                        cursor = Cursors.No;
                        break;
                    }
                }
            }
            Cursor = cursor;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (Animation == null) return;
            if (_DragCP != null)
            {
                _ControlPoints.MoveToTop(_DragCP);
                _DragCP = null;
            }
            //var ellipse = Animation.Item as AnimationEllipse;
            var path = Animation.Item as AnimationPath;
            if (Mode == AnimationEditorMode.AddPathPoint && path != null)
            {
                if (_SafeRegion.Contains(e.X, e.Y))
                {
                    var pt = _ControlPoints.AddLocationPoint(new PointF(e.X, e.Y), Cursors.SizeAll);
                    path.PointList.Add(new AnimationPathPoint() { X = pt.X, Y = pt.Y });
                    Invalidate();
                }
            }
            if (Mode == AnimationEditorMode.RemovePathPoint && path != null)
            {
                foreach (var cp in _ControlPoints.OrderBy(cp => cp.ZIndex))
                {
                    if (cp.HitTest(e.Location))
                    {
                        var index = _ControlPoints.IndexOf(cp);
                        if (index > -1)
                        {
                            _ControlPoints.Remove(cp);
                            path.PointList.RemoveAt(index);
                            Invalidate();
                        }
                        break;
                    }
                }
            }
            Cursor = Cursors.Default;
        }

        #endregion

        #region Rendering

        private ControlPointCollection _ControlPoints;
        private object _CPShape;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Scale bounding box to canvas
            var scaledBox = RectangleF.Empty;
            var scale = 1.0f;

            lock (_LockObject)
            {
                if (!_CurrentBounds.IsEmpty)
                {
                    var vScale = (Height - 33) / _CurrentBounds.Height;
                    var hScale = (Width - 33) / _CurrentBounds.Width;
                    scale = new float[] { vScale, hScale }.Min();
                    var newH = _CurrentBounds.Height * scale;
                    var newW = _CurrentBounds.Width * scale;
                    scaledBox = new RectangleF((Width - newW) / 2, (Height - newH) / 2, newW, newH);
                }

                _SafeRegion = new RectangleF(scaledBox.X + 8, scaledBox.Y + 8, scaledBox.Width, scaledBox.Height);
                if (_ControlPoints != null && _SafeRegion != _ControlPoints.SafeRegion) _ControlPoints = null; // Force control point regeneration

                var p = new Pen(SystemColors.WindowFrame);
                p.DashStyle = DashStyle.Dash;

                // Draw scaled container
                e.Graphics.DrawRectangle(p, scaledBox.X, scaledBox.Y, scaledBox.Width + 16, scaledBox.Height + 16);

                if (Animation != null)
                {
                    if (_Animation.Item != _CPShape)
                    {
                        _CPShape = Animation.Item;
                        _ControlPoints = null;
                    }

                    if (_ControlPoints == null)
                    {
                        // Create CPs
                        var ellipse = _CPShape as AnimationEllipse;
                        var path = _CPShape as AnimationPath;
                        if (ellipse != null)
                        {
                            _ControlPoints = new RegionControlPointCollection(_SafeRegion, ellipse);
                        }
                        else if (path != null)
                        {
                            _ControlPoints = new FreeControlPointCollection(_SafeRegion, path);
                        }
                    }

                    if (Mode == AnimationEditorMode.Preview)
                    {
                        var loc = new PointF(scaledBox.X + _CurrentPoint.X * scale, scaledBox.Y + _CurrentPoint.Y * scale);
                        e.Graphics.DrawImage(_PreviewIcon, loc);
                    }
                    else
                    {
                        for (float progress = 0; progress < 1; progress += 0.01f)
                        {
                            var pt = IdleMovementManager.Interpolate(Animation, _CurrentBounds, progress);
                            var loc = new PointF(scaledBox.X + pt.X * scale, scaledBox.Y + pt.Y * scale);
                            e.Graphics.DrawImage(_Icon, loc);
                        }

                        var controlPointFill = new SolidBrush(SystemColors.Control);
                        var controlPointStroke = new Pen(SystemColors.ControlText);
                        var ellipse = Animation.Item as AnimationEllipse;
                        var path = Animation.Item as AnimationPath;

                        // Draw path lines or ellipse container lines
                        if (ellipse != null)
                        {
                            var ellipseContainerX = _SafeRegion.X + (ellipse.CX - ellipse.RX) * _SafeRegion.Width;
                            var ellipseContainerY = _SafeRegion.Y + (ellipse.CY - ellipse.RY) * _SafeRegion.Height;
                            var ellipseContainerWidth = ellipse.RX * 2 * _SafeRegion.Width;
                            var ellipseContainerHeight = ellipse.RY * 2 * _SafeRegion.Height;
                            e.Graphics.DrawRectangle(p, ellipseContainerX, ellipseContainerY, ellipseContainerWidth, ellipseContainerHeight);
                        }
                        else if (path != null && path.PointList.Count > 1)
                        {
                            var c = path.PointList.Count;
                            for (int i = 1; i < c; i++)
                            {
                                var pathPtA = path.PointList[i];
                                var pathPtB = path.PointList[i - 1];
                                var ptA = new PointF(_SafeRegion.X + pathPtA.X * _SafeRegion.Width, _SafeRegion.Y + pathPtA.Y * _SafeRegion.Height);
                                var ptB = new PointF(_SafeRegion.X + pathPtB.X * _SafeRegion.Width, _SafeRegion.Y + pathPtB.Y * _SafeRegion.Height);
                                e.Graphics.DrawLine(p, ptA, ptB);
                            }
                        }

                        // Draw control points
                        foreach (var cp in _ControlPoints.OrderBy(cp => cp.ZIndex))
                        {
                            e.Graphics.FillEllipse(controlPointFill, cp.Region);
                            e.Graphics.DrawEllipse(controlPointStroke, cp.Region);
                        }
                    }
                }
            }
        }

        #endregion

        #region Private members

        private void TryInvoke(Action action)
        {
            if (IsDisposed || !IsHandleCreated) return;
            try
            {
                Invoke(action);
            }
            catch (ObjectDisposedException) { } // Ignore
        }

        private void AnimationThread()
        {
            var durStart = DateTime.Now;
            while (true)
            {
                var frameStart = DateTime.Now;
                if (frameStart - durStart > _CurrentDuration) durStart = frameStart;
                float progress = (float)((frameStart - durStart).TotalMilliseconds / _CurrentDuration.TotalMilliseconds);

                bool doInvalidate = false;
                lock (_LockObject)
                {
                    if (_Animation != null && _Mode == AnimationEditorMode.Preview)
                    {
                        _CurrentPoint = IdleMovementManager.Interpolate(_Animation, _CurrentBounds, progress);
                        doInvalidate = true;
                    }
                }

                if (doInvalidate) TryInvoke(Invalidate);

                var frameDur = DateTime.Now - frameStart;
                if (frameDur < DEFAULT_FRAME_DURATION) Thread.Sleep(DEFAULT_FRAME_DURATION - frameDur);
            }
        }

        #endregion
    }

    internal enum AnimationEditorMode
    {
        Select, AddPathPoint, RemovePathPoint, Preview
    }
}
