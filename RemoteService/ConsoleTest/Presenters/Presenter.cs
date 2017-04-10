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

        public void Launch(string processName, string filePath)
        {
            string quotes = "\"";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = processName;
            startInfo.Arguments = "/o " + quotes + filePath + quotes;
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;

            process = Process.Start(startInfo);
        }

        public virtual void Rendering(string path, string savePath, int dpi)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }

        public int getSlidesCount()
        {
            return count;
        }

        public abstract string[] getKeys();

        public abstract Process getProcess();
    }
}
