using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//СЛЕДИТЬ ЗА ПРИЛОЖЕНИЕМ ЛУЧШЕ ЧЕРЕЗ ОБЫЧНЫЕ "ОКНА" WINDOWS

namespace ConsoleTest.Presenters
{
    abstract class Presenter
    {
        protected Process process;
        protected int count;
        protected string[] keys;
        protected string savePath;
        protected string extension;
        protected string format;

        public void Launch(string processName, string filePath)
        {
            string quotes = "\"";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = processName + ".exe";
            startInfo.Arguments = "/o " + quotes + filePath + quotes;
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;

            process = Process.Start(startInfo);
        }

        public void CreateDirectory(string savePath)
        {
            this.savePath = savePath;

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }

        public abstract void Configure(string filePath, int dpi);

        public abstract void SavePageRendering(int index);

        public string GetSavePath() { return savePath; }

        public int GetSlidesCount() { return count; }

        public string GetKey(int index) { return keys[index]; }

        public abstract string GetCommandGoPage(string code);

        public string GetExtension() { return extension; }

        public abstract void SetProcess();

        public Process GetProcess() { return process; }

        public abstract void Clear();
    }
}
