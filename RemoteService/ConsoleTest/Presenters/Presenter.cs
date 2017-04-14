using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ПРОДУМАТЬ МОМЕНТ: ПОЛЬЗОВАТЕЛЬ ЗАКРОЕТ ПРЕЗЕНТАЦИЮ СРАЗУ ПОСЛЕ ОТКРЫТИЯ

namespace ConsoleTest.Presenters
{
    abstract class Presenter
    {
        protected Process process;
        protected int count;
        protected string[] keys;
        protected string savePath;
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

        public string GetFormat() { return format; }

        public abstract Process GetProcess();

        public abstract void Clear();
    }
}
