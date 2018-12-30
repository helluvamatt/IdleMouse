using System;
using System.Drawing;

namespace IdleMouse.Models
{
    public class MoveEventArgs : EventArgs
    {
        public MoveEventArgs(string animationName, float animationProgress, RectangleF bounds, PointF point)
        {
            AnimationName = animationName;
            AnimationProgress = animationProgress;
            Bounds = bounds;
            Point = point;
        }

        public string AnimationName { get; }

        public float AnimationProgress { get; }

        public RectangleF Bounds { get; }

        public PointF Point { get; }
    }
}
