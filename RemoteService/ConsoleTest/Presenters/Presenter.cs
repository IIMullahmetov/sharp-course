using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleTest.Presenters
{
    abstract class Presenter
    {
        protected Process process;
        protected ManualResetEvent createEvent;
        protected string[] keys;
        protected string presentationPath { get; set; }
        protected string savePath;
        protected string extension;
        protected string presentationName;
        protected string format;
        protected string processName;
        protected string programName;
        protected int dpi;
        protected int processId;
        protected int count;

        //Переменные для определенных программ
        protected string presentationWindowName;
        protected ImageFormat imageFormat;

        public Presenter() { }

        public Presenter(ManualResetEvent createEvent, string savePath, string extension, int dpi)
        {
            this.createEvent = createEvent;
            this.savePath = savePath;
            this.extension = extension;
            this.dpi = dpi;
            format = extension.Substring(1, extension.Length - 1);
        }

        public void Launch()
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
            if (Directory.Exists(savePath))
            {
                createEvent.WaitOne();
                Directory.Delete(savePath, true);
                createEvent.Reset();
            }
        }

        public void CreateDirectory()
        {
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
        }

        public abstract void SetParametres();

        public virtual void LaunchNewPresentation(string presentationPath)
        {
            DeleteDirectory();
            this.presentationPath = presentationPath;
            presentationName = Path.GetFileNameWithoutExtension(this.presentationPath);
            CreateDirectory();
        }

        public abstract void ConfigureRendering();

        public abstract void SavePageRendering(int index);

        public abstract void SavePagesRendering(Socket handler);

        public string GetSavePath() { return savePath; }

        public string GetPresentationName() { return presentationName; }

        public int GetSlidesCount() { return count; }

        public string GetKey(int index) { return keys[index]; }

        public abstract string GetCommandGoPage(int code);

        public abstract string GetCommandExitProgram();

        public string GetExtension() { return extension; }

        public string GetProgramName() { return programName; }

        public string GetPresentationWindowName() { return presentationWindowName; }

        public  void SetProcessId(int processId) { this.processId = processId; }

        public int GetProcessId() { return processId; }

        public abstract void Clear();
    }
}
