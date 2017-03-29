using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSupergoo.ABCpdf10;

namespace ConsoleTest.Presenters
{
    class AdobeReaderProgram
    {
        string[] keys = { "{RIGHT}", "{LEFT}", "^(l)", "{ESC}", ""};

        int count;

        public AdobeReaderProgram(string path, string savePath)
        {
            Launch(path);
            Rendering(path, savePath);
        }

        public void Launch(string path)
        {
            string quotes = "\"";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "AcroRd32.exe";
            startInfo.Arguments = "/n " + quotes + path + quotes;
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;

            Process process = Process.Start(startInfo);
        }

        public void Rendering(string path, string savePath)
        {
            Doc theDoc = new Doc();
            theDoc.Read(path);
            theDoc.Rendering.DotsPerInch = 96;
            count = theDoc.PageCount;

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            for (int i = 1; i <= theDoc.PageCount; i++)
            {
                theDoc.PageNumber = i;
                theDoc.Rect.String = theDoc.CropBox.String;
                theDoc.Rendering.Save(savePath + i.ToString() + ".jpg");
            }
            theDoc.Clear();
        }

        public int getSlidesCount()
        {
            return count;
        }

        public string[] getKeys()
        {
            return keys;
        }
    }
}
