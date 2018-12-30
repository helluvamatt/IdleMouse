using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IdleMouse.Controls
{
    internal class DoubleBufferedListBox : ListBox
    {
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var currentContext = BufferedGraphicsManager.Current;
            var newBounds = new Rectangle(0, 0, e.Bounds.Width, e.Bounds.Height);
            using (var bufferedGraphics = currentContext.Allocate(e.Graphics, newBounds))
            {
                var newArgs = new DrawItemEventArgs(bufferedGraphics.Graphics, e.Font, newBounds, e.Index, e.State, e.ForeColor, e.BackColor);
                base.OnDrawItem(newArgs);
                GDI.CopyGraphics(e.Graphics, e.Bounds, bufferedGraphics.Graphics, new Point(0, 0));
            }
        }

        private static class GDI
        {
            private const uint SRCCOPY = 0x00CC0020;

            [DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall)]
            private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

            public static void CopyGraphics(Graphics g, Rectangle bounds, Graphics bufferedGraphics, Point p)
            {
                IntPtr hdc1 = g.GetHdc();
                IntPtr hdc2 = bufferedGraphics.GetHdc();
                BitBlt(hdc1, bounds.X, bounds.Y, bounds.Width, bounds.Height, hdc2, p.X, p.Y, SRCCOPY);
                g.ReleaseHdc(hdc1);
                bufferedGraphics.ReleaseHdc(hdc2);
            }
        }
    }
}
