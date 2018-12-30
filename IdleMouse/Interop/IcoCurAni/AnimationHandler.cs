using System;
using System.Drawing;
using System.Threading;

namespace IdleMouse.Interop.IcoCurAni
{
    internal class AnimationHandler : IDisposable
    {
        private static readonly TimeSpan DEFAULT_FRAME_DURATION = TimeSpan.FromSeconds(1/60.0);

        private static readonly Thread _AnimationTimer;

        private static event Action OnAnimationTick;

        static AnimationHandler()
        {
            _AnimationTimer = new Thread(AnimationTick);
            _AnimationTimer.Name = "Cursor Animation Thread";
            _AnimationTimer.IsBackground = true;
            _AnimationTimer.Start();
        }

        private readonly Action<bool> _OnAnimationTick;
        private uint _TickIndex = 0;
        private int _FrameIndex = 0;
        private object _LockObject = new { };
        
        public AnimationHandler(Action<bool> animationTick)
        {
            _OnAnimationTick = animationTick;
            OnAnimationTick += OnAnimTick;
        }

        private CursorModel _Cursor;
        public CursorModel Cursor
        {
            get => _Cursor;
            set
            {
                if (_Cursor != value)
                {
                    lock (_LockObject)
                    {
                        _Cursor = value;
                        _FrameIndex = 0;
                        _TickIndex = 0;
                    }
                }
            }
        }

        public Image GetCurrentFrame()
        {
            if (_Cursor == null || _Cursor.Frames == null || _Cursor.FrameNums == null || _Cursor.Frames.Length < 1 || _Cursor.FrameNums.Length < 1) return null;
            return _Cursor.Frames[_Cursor.FrameNums[_FrameIndex]].Frame;
        }

        private static void AnimationTick()
        {
            while (true)
            {
                var frameStart = DateTime.Now;
                OnAnimationTick?.Invoke();
                var frameDur = DateTime.Now - frameStart;
                if (frameDur < DEFAULT_FRAME_DURATION) Thread.Sleep(DEFAULT_FRAME_DURATION - frameDur);
            }
        }

        private void OnAnimTick()
        {
            if (_Cursor == null) return;

            _TickIndex++;

            bool frameChanged = false;
            lock (_LockObject)
            {
                if (_TickIndex >= _Cursor.FrameRates[_FrameIndex])
                {
                    _TickIndex = 0;
                    _FrameIndex++;
                    if (_FrameIndex >= _Cursor.Frames.Length) _FrameIndex = 0;
                    frameChanged = true;
                }
            }

            _OnAnimationTick(frameChanged);
        }

        public void Dispose()
        {
            OnAnimationTick -= OnAnimTick;
        }
    }
}
