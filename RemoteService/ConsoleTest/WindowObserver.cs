﻿using ConsoleTest.Presenters;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ConsoleTest
{
    class WindowObserver
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        //static Process currentProcess;

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
            string currentProcess = Process.GetProcessById(currentProcessId).MainWindowTitle; //получаем процесс активного окна
            if (currentProcess.StartsWith(presenter.GetPresentationName()) && currentProcess.EndsWith(presenter.GetProgramName())
                || (presenter is PowerPointProgram && currentProcess.StartsWith(presenter.GetPresentationWindowName())))
                return true;
            else
                return false;
        }
    }
}
