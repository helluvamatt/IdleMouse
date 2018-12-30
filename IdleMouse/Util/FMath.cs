using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleMouse.Util
{
    public static class FMath
    {
        public const float PI = 3.14159265358979323846f;

        public const float TwoPI = PI * 2f;

        public const float PI_2 = PI / 2.0f;

        public static float Sin(float a)
        {
            return (float)Math.Sin(a);
        }

        public static float Cos(float a)
        {
            return (float)Math.Cos(a);
        }

        public static float Pow(float x, float y)
        {
            return (float)Math.Pow(x, y);
        }

        public static float Sqrt(float x)
        {
            return (float)Math.Sqrt(x);
        }

        public static float Segment(float p, int segmentCount, out int currentSegment)
        {
            if (p < 1 && p >= 0)
            {
                float v = p * segmentCount;
                currentSegment = (int)Math.Floor(v);
                return v - currentSegment;
            }
            else
            {
                currentSegment = 0;
                return 0;
            }
        }

        /// <summary>
        /// Integral method for computing the circumference of an ellipse using 1000 segments/iterations
        /// </summary>
        /// <param name="rX">Major axis</param>
        /// <param name="rY">Minor axis</param>
        /// <returns>Circumference of an ellipse</returns>
        /// <remarks>
        /// This method uses double-precision internally for accuracy, casting to single-precision just before returning
        /// </remarks>
        public static float CircEllipse(float rX, float rY)
        {
            // Eccentricity
            double e = Math.Sqrt(1 - (rX * rX) / (rY * rY));

            // Integral method for computing the circumference of an ellipse using 1000 segments/iterations
            int n = 1000;
            double len = 0;
            double dt = 0.5 * PI / n;
            for (int i = 0; i < n; i++)
            {
                double theta = 0.5 * PI * (i + 0.5) / n;
                len += Math.Sqrt(1 - e * e * Math.Sin(theta) * Math.Sin(theta)) * dt;
            }
            len *= 4 * rX;
            return (float)len;
        }

        /// <summary>
        /// Compute the Euclidian distance between two points in two-dimensional space
        /// </summary>
        /// <param name="x1">"A" X-coordinate</param>
        /// <param name="y1">"A" Y-coordinate</param>
        /// <param name="x2">"B" X-coordinate</param>
        /// <param name="y2">"B" y-coordinate</param>
        /// <returns>Euclidian distance between the given points</returns>
        public static float EuclDist(float x1, float y1, float x2, float y2)
        {
            float a = Math.Abs(x2 - x1);
            float b = Math.Abs(y2 - y1);
            return Sqrt(a * a + b * b);
        }
    }
}
