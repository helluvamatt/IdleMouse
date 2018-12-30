using IdleMouse.Interop;
using System;
using System.Windows.Forms;

namespace IdleMouse.Util
{
    internal static class FormUtils
    {
        public static void BringToFront(this Form form, bool flash)
        {
            // get our current "TopMost" value
            bool top = form.TopMost;
            // make our form jump to the top of everything
            form.TopMost = true;
            // set it back to whatever it was
            form.TopMost = top;

            if (form.IsHandleCreated && flash) Win32.FlashWindow(form.Handle, FlashWinInfoFlags.FLASHW_ALL, 5, 100);
        }

        public static string SizeToString(long size, int decimalPlaces = 2)
        {
            string[] suf = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (size == 0) return "0 " + suf[0];
            long bytes = Math.Abs(size);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), decimalPlaces);
            return (Math.Sign(size) * num).ToString() + " " + suf[place];
        }
    }
}
