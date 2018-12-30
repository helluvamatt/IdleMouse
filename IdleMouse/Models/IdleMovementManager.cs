using IdleMouse.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Settings = IdleMouse.Properties.Settings;

namespace IdleMouse.Models
{
    internal class IdleMovementManager
    {
        private const ushort MIN_DURATION = 100; // ms
        private const ushort MAX_SPEED = 10000; // -ms
        private const float NORMAL_DISTANCE = 500;
        private static readonly TimeSpan DEFAULT_FRAME_DURATION = TimeSpan.FromMilliseconds(25);

        private const string NEW_ANIMATION_PREFIX = "New Animation ";

        private readonly AnimationCollection _DefaultAnimations;
        private readonly List<Animation> _Animations;

        private readonly string _UserConfigFile;

        private Thread _AnimationThread;
        private bool _Running = false;
        private readonly object _LockObject = new { };
        private int _CurrentIndex = -1;
        private RectangleF _CurrentBounds;
        private TimeSpan _CurrentDuration;

        #region Events

        public event EventHandler<MoveEventArgs> Move;

        protected void OnMove(string animationName, float animationProgress, RectangleF bounds, PointF point)
        {
            Move?.Invoke(this, new MoveEventArgs(animationName, animationProgress, bounds, point));
        }

        #endregion

        public BindingList<Animation> Animations { get; }

        public int GetCurrentAnimationIndex()
        {
            return _CurrentIndex;
        }

        public Animation GetCurrentAnimation()
        {
            if (_CurrentIndex > -1 && _CurrentIndex < _Animations.Count)
            {
                return _Animations[_CurrentIndex];
            }
            return null;
        }

        public IdleMovementManager(string userConfigFile)
        {
            _UserConfigFile = userConfigFile;
            _Animations = new List<Animation>();
            Animations = new BindingList<Animation>(_Animations);
            Animations.AddingNew += Animations_AddingNew;
            Animations.ListChanged += Animations_ListChanged;

            _DefaultAnimations = ConfigurationManager.GetSection("animations") as AnimationCollection;
            if (_DefaultAnimations == null)
            {
                throw new Exception("Failed to load default configuration.");
            }

            _CurrentBounds = new RectangleF(0, 0, Settings.Default.IdleAnimationWidth, Settings.Default.IdleAnimationHeight);
            _CurrentDuration = ComputeAnimationDuration(null, _CurrentBounds, Settings.Default.IdleAnimationSpeed);

            Settings.Default.PropertyChanged += Settings_PropertyChanged;
        }

        #region Event handlers

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.IdleAnimation))
            {
                lock (_LockObject)
                {
                    _CurrentIndex = Settings.Default.IdleAnimation;
                    _CurrentDuration = ComputeAnimationDuration(GetCurrentAnimation(), _CurrentBounds, Settings.Default.IdleAnimationSpeed);
                }
            }
            if (e.PropertyName == nameof(Settings.IdleAnimationHeight) || e.PropertyName == nameof(Settings.IdleAnimationWidth))
            {
                lock (_LockObject)
                {
                    _CurrentBounds = new RectangleF(0, 0, Settings.Default.IdleAnimationWidth, Settings.Default.IdleAnimationHeight);
                    _CurrentDuration = ComputeAnimationDuration(GetCurrentAnimation(), _CurrentBounds, Settings.Default.IdleAnimationSpeed);
                }
            }
            if (e.PropertyName == nameof(Settings.IdleAnimationSpeed))
            {
                lock (_LockObject)
                {
                    _CurrentDuration = ComputeAnimationDuration(GetCurrentAnimation(), _CurrentBounds, Settings.Default.IdleAnimationSpeed);
                }
            }
        }

        private void Animations_AddingNew(object sender, AddingNewEventArgs e)
        {
            int newNum = Animations.Where(a => a.Name.StartsWith(NEW_ANIMATION_PREFIX)).Count() + 1;

            var newAnim = new Animation();
            newAnim.EasingFunction = Easings.Functions.Linear;
            newAnim.Item = new AnimationPath();
            newAnim.Name = "New Animation " + newNum;
            e.NewObject = newAnim;
        }

        private void Animations_ListChanged(object sender, ListChangedEventArgs e)
        {
            SaveAnimations();
        }

        #endregion

        public void LoadAnimations()
        {
            Animations.RaiseListChangedEvents = false;
            try
            {
                if (File.Exists(_UserConfigFile))
                {
                    var userAnimations = AnimationsConfigSectionHandler.LoadFromFile(_UserConfigFile);
                    if (userAnimations == null) throw new InvalidOperationException("XML was malformed."); // Should never happen, XML parsing should fail first
                    _Animations.AddRange(userAnimations.Animations);
                }
                else
                {
                    _Animations.AddRange(_DefaultAnimations.Animations);
                    SaveAnimations();
                }
                _CurrentIndex = Settings.Default.IdleAnimation;
                _CurrentDuration = ComputeAnimationDuration(GetCurrentAnimation(), _CurrentBounds, Settings.Default.IdleAnimationSpeed);
            }
            finally
            {
                Animations.RaiseListChangedEvents = true;
            }
        }

        public void SaveAnimations()
        {
            AnimationsConfigSectionHandler.SaveToFile(_UserConfigFile, new AnimationCollection() { Animations = _Animations.ToArray() });
        }

        public void AddAnimation(Animation animation)
        {
            _Animations.Add(animation);
        }

        public void Start()
        {
            if (_Running) return;
            _Running = true;
            _AnimationThread = new Thread(Animate);
            _AnimationThread.Name = "Animation Thread";
            _AnimationThread.IsBackground = true;
            _AnimationThread.Start();
        }

        public void Stop()
        {
            if (!_Running) return;
            _Running = false;
            _AnimationThread.Join();
            _AnimationThread = null;
        }

        public static PointF Interpolate(Animation animation, RectangleF bounds, float progress)
        {
            if (progress > 1) progress = 1;
            if (progress < 0) progress = 0;
            AnimationPath path;
            AnimationEllipse ellipse;
            if ((path = animation.Item as AnimationPath) != null)
            {
                if (path.Points.Length < 1) return new PointF(0, 0); // Short-circuit empty points array
                if (path.InterpolationMode == InterpolationMode.Normal) progress = Easings.Interpolate(progress, animation.EasingFunction);
                int startIndex;
                var p = FMath.Segment(progress, path.Points.Length, out startIndex);
                if (path.InterpolationMode == InterpolationMode.Repeat || (path.InterpolationMode == InterpolationMode.Alternate && startIndex % 2 == 0)) p = Easings.Interpolate(p, animation.EasingFunction);
                else if (path.InterpolationMode == InterpolationMode.Alternate && startIndex % 2 == 1) p = 1 - Easings.Interpolate(1 - p, animation.EasingFunction);
                AnimationPathPoint startPt, endPt;
                if (animation.Reverse)
                {
                    startPt = startIndex > 0 ? path.Points[path.Points.Length - startIndex] : path.Points[0];
                    endPt = path.Points[path.Points.Length - startIndex - 1];
                }
                else
                {
                    startPt = path.Points[startIndex];
                    endPt = startIndex < (path.Points.Length - 1) ? path.Points[startIndex + 1] : path.Points[0];
                }
                var x = (endPt.X - startPt.X) * p + startPt.X;
                var y = (endPt.Y - startPt.Y) * p + startPt.Y;
                return new PointF(bounds.X + bounds.Width * x, bounds.Y + bounds.Height * y);
            }
            else if ((ellipse = animation.Item as AnimationEllipse) != null)
            {
                progress = Easings.Interpolate(progress, animation.EasingFunction);
                if (animation.Reverse) progress = 1 - progress;
                var angle = FMath.TwoPI - (progress * FMath.TwoPI) + FMath.PI_2;
                var center = new PointF(bounds.X + ellipse.CX * bounds.Width, bounds.Y + ellipse.CY * bounds.Height);
                var x = FMath.Cos(angle) * ellipse.RX * bounds.Width;
                var y = -FMath.Sin(angle) * ellipse.RY * bounds.Height;
                return PointF.Add(center, new SizeF(x, y));
            }
            else
            {
                // Should never happen if XML parsing worked properly
                return bounds.Location;
            }
        }

        public static TimeSpan ComputeAnimationDuration(Animation animation, RectangleF bounds, ushort speed)
        {
            // Base formula: determine milliseconds to travel the "normal" distance
            float duration = MAX_SPEED + MIN_DURATION - Math.Min(MAX_SPEED, speed);

            if (animation != null)
            {
                AnimationEllipse ellipse;
                AnimationPath path;
                float length = 0;
                if ((path = animation.Item as AnimationPath) != null)
                {
                    for (int i = 1; i < path.Points.Length; i++)
                    {
                        var startPt = path.Points[i - 1];
                        var endPt = path.Points[i];
                        length += FMath.EuclDist(startPt.X * bounds.Width, startPt.Y * bounds.Height, endPt.X * bounds.Width, endPt.Y * bounds.Height);
                    }
                }
                else if ((ellipse = animation.Item as AnimationEllipse) != null)
                {
                    length = FMath.CircEllipse(ellipse.RX * bounds.Width, ellipse.RY * bounds.Height);
                }
                duration *= (length / NORMAL_DISTANCE);
            }

            return TimeSpan.FromMilliseconds(duration);
        }

        #region Animation loop thread

        private void Animate()
        {
            var durStart = DateTime.Now;
            while (_Running)
            {
                var frameStart = DateTime.Now;
                if (frameStart - durStart > _CurrentDuration) durStart = frameStart;
                float progress = (float)((frameStart - durStart).TotalMilliseconds / _CurrentDuration.TotalMilliseconds);

                PointF? pt = null;
                string name = null;
                RectangleF bounds;
                lock (_LockObject)
                {
                    bounds = _CurrentBounds;
                    if (_CurrentIndex > -1 && _CurrentIndex < _Animations.Count)
                    {
                        var a = _Animations[_CurrentIndex];
                        name = a.Name;
                        pt = Interpolate(a, bounds, progress);
                    }
                }

                if (pt.HasValue)
                {
                    OnMove(name, progress, bounds, pt.Value);
                }

                var frameDur = DateTime.Now - frameStart;
                if (frameDur < DEFAULT_FRAME_DURATION) Thread.Sleep(DEFAULT_FRAME_DURATION - frameDur);
            }
        }

        #endregion
    }
}
