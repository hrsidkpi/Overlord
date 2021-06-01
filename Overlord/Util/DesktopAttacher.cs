using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace Overlord.Util
{
    public static class DesktopAttacher
	{

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public static void SetAsDesktop(Window window)
        {
            IntPtr hWnd = new WindowInteropHelper(window).Handle;
            IntPtr hWndProgMan = FindWindow("Progman", "Program Manager");
            SetParent(hWnd, hWndProgMan);

        }

    }
}
