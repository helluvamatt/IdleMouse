using IdleMouse.Interop;
using IdleMouse.Models;
using Mono.Options;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using R = IdleMouse.Properties.Resources;

namespace IdleMouse
{
    public static class Program
    {
        private static readonly Mutex mutex = new Mutex(true, "{E33E0628-DDFB-4CD3-BE1F-AEFF84621EA2}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            bool help = false;
            bool showMainForm = true;
            bool reset = false;
            var opts = new OptionSet
            {
                { "h|help", "Show help and exit.", h => help = h != null },
                { "s|startup", "Startup mode, don't show the configuration window.", s => showMainForm = s == null },
                { "r|reset", "Reset user_animations.xml", r => reset = r != null }
            };
            opts.Parse(args);

            if (help)
            {
                opts.WriteOptionDescriptions(Console.Out);
                return;
            }

            var userConfigFile = Path.Combine(Application.LocalUserAppDataPath, "user_animations.xml");
            if (reset)
            {
                try
                {
                    File.Delete(userConfigFile);
                    Console.WriteLine("Cleared user configuration.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(R.ErrorFailedToDeleteUserConfiguration + ex.Message, R.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                IdleMovementManager movementManager = null;
                try
                {
                    movementManager = new IdleMovementManager(userConfigFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(R.ErrorFailedToParseConfiguration + ex.Message, R.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    movementManager.LoadAnimations();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(R.ErrorFailedToParseUserAnimations + ex.Message, R.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                CursorManager cursorManager = null;
                try
                {
                    cursorManager = new CursorManager();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(R.ErrorFailedToLoadCursor + ex.Message, R.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Application.Run(new IdleMouseApplication(showMainForm, movementManager, cursorManager));
            }
            else
            {
                Win32.PostMessageA(new IntPtr(Win32.HWND_BROADCAST), Win32.WM_APP_SHOWCONFIG, UIntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
