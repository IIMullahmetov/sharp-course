using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Shared.Server.Services
{
    public class WindowService
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        public static bool IsPresentationWindow(Presenter presenter)
        {
            int currentProcessId;
            IntPtr handle = GetForegroundWindow(); // получаем хендел активного окна
            GetWindowThreadProcessId(handle, out currentProcessId); //получаем currentProcessId потока активного окна
            if (currentProcessId == presenter.GetProcessId())
                return true;
            else
                return false;
        }

        public static bool IsMyPresentation(Presenter presenter)
        {
            int currentProcessId;
            IntPtr handle = GetForegroundWindow(); // получаем хендел активного окна
            GetWindowThreadProcessId(handle, out currentProcessId); //получаем currentProcessId потока активного окна
            string currentProcessTitle = Process.GetProcessById(currentProcessId).MainWindowTitle; //получаем процесс активного окна
            if (currentProcessTitle.StartsWith(presenter.GetPresentationName()) && currentProcessTitle.EndsWith(presenter.GetProgramName())
                || (presenter is PowerPointProgram && currentProcessTitle.StartsWith(presenter.GetPresentationWindowName())))
            {
                return true;
            }
            else
                return false;
        }
    }
}
