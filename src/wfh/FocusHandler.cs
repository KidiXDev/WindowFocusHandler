using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

namespace wfh
{
    public class FocusHandler
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);

        [DllImport("user32.dll")]
        private static extern bool GetGUIThreadInfo(uint idThread, ref GUITHREADINFO lpgui);

        [StructLayout(LayoutKind.Sequential)]
        private struct GUITHREADINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public Rectangle rcCaret;
        }

        /// <summary>
        /// Checks if the specified process window is focused.
        /// </summary>
        /// <param name="process">The process to check for focus.</param>
        /// <returns>True if the process window is focused, otherwise false.</returns>
        public static bool IsProcessWindowFocused(Process process)
        {
            if (process == null)
                return false;

            IntPtr hwnd = GetForegroundWindow();

            // Get the process ID of the window
            uint processId;
            GetWindowThreadProcessId(hwnd, out processId);

            if (processId != 0 && processId == process.Id)
            {
                // Get GUI thread information
                GUITHREADINFO guiInfo = new GUITHREADINFO();
                guiInfo.cbSize = Marshal.SizeOf(guiInfo);
                GetGUIThreadInfo(0, ref guiInfo);

                // Return true if the active window is the same as the process's window
                return guiInfo.hwndActive == hwnd;
            }

            // If the process ID does not match, return false
            return false;
        }

    }
}
