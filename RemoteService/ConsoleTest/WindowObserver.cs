using ConsoleTest.Presenters;
using System;
using System.Runtime.InteropServices;

namespace ConsoleTest
{
    class WindowObserver
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        public static bool IsPresentationWindow(Presenter presenter)
        {
            int currentProcessId;
            IntPtr handle = GetForegroundWindow(); // получаем хендел активного окна
            GetWindowThreadProcessId(handle, out currentProcessId); //получаем currentProcessId потока активного окна
            if (currentProcessId == presenter.GetProcess().Id)
                return true;
            else
                return false;
        }
    }
}
