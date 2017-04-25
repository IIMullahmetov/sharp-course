using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//НУЖНО ЗАВЕРШАТЬ ПРОЦЕСС POWERPNT

namespace ConsoleTest.Presenters
{
    abstract class Presenter
    {
        protected Process process;
        protected ManualResetEvent createEvent;
        protected int processId;
        protected int count;
        protected string[] keys;
        protected string savePath;
        protected string presentationName;
        protected string extension;
        protected string format;
        protected string programName;
        protected string presentationWindowName;

        public void Launch(string processName, string filePath)
        {
            string quotes = "\"";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = processName + ".exe";
            startInfo.Arguments = "/o " + quotes + filePath + quotes;
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;

            process = Process.Start(startInfo);
        }

        public void DeleteDirectory(string savePath)
        {
            this.savePath = savePath;
            if (Directory.Exists(savePath))
                Directory.Delete(savePath, true);
        }

        public void CreateDirectory(string savePath)
        {
            Directory.CreateDirectory(savePath);
        }

        public abstract void Configure(string filePath, int dpi);

        public abstract void SavePageRendering(int index);

        public abstract void SavePagesRendering(Socket handler);

        public string GetSavePath() { return savePath; }

        public string GetPresentationName() { return presentationName; }

        public int GetSlidesCount() { return count; }

        public string GetKey(int index) { return keys[index]; }

        public abstract string GetCommandGoPage(int code);

        public string GetExtension() { return extension; }

        public string GetProgramName() { return programName; }

        public string GetPresentationWindowName() { return presentationWindowName; }

        public abstract void SetProcessId();

        public int GetProcessId() { return processId; }

        public abstract void Clear();
    }
}
