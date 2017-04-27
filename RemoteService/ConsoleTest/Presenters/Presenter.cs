using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Threading;

//НУЖНО ЗАВЕРШАТЬ ПРОЦЕСС POWERPNT

namespace ConsoleTest.Presenters
{
    abstract class Presenter
    {
        protected Process process;
        protected ManualResetEvent createEvent;
        protected string[] keys;
        protected string presentationPath;
        protected string savePath;
        protected string extension;
        protected string presentationName;
        protected string format;
        protected string processName;
        protected string programName;
        protected int dpi;
        protected int processId;
        protected int count;

        protected string presentationWindowName;
        protected ImageFormat imageFormat;

        public Presenter(ManualResetEvent createEvent, string presentationPath, string savePath, string extension, int dpi)
        {
            this.createEvent = createEvent;
            this.presentationPath = presentationPath;
            this.savePath = savePath;
            this.extension = extension;
            this.dpi = dpi;
            presentationName = Path.GetFileNameWithoutExtension(this.presentationPath);
            format = extension.Substring(1, extension.Length - 1);
        }

        public void Launch()
        {
            DeleteDirectory();
            StartProcess();
            CreateDirectory();
        }

        public void StartProcess()
        {
            const string quote = "\"";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = processName + ".exe";
            startInfo.Arguments = "/o " + quote + presentationPath + quote;
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;

            process = Process.Start(startInfo);
        }

        public void DeleteDirectory()
        {
            try
            {
                if (Directory.Exists(savePath))
                    Directory.Delete(savePath, true);
            }
            catch (IOException)
            {
                Thread.Sleep(10);
                DeleteDirectory();
            }
        }

        public void CreateDirectory()
        {
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
        }

        public abstract void ConfigureRendering();

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

        public  void SetProcessId(int processId) { this.processId = processId; }

        public int GetProcessId() { return processId; }

        public abstract void Clear();
    }
}
