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
    class AdobeReaderProgram : Presenter
    {
        string processName = "AcroRd32";

        new string[] keys = { "{RIGHT}", "{LEFT}", "^(l)", "{ESC}", "^(+n)" };

        public AdobeReaderProgram(string filePath, string savePath, int dpi)
        {
            base.keys = keys;
            Launch(processName, filePath);
            Rendering(filePath, savePath, dpi);
        }

        public override void Rendering(string filePath, string savePath, int dpi)
        {
            base.Rendering(filePath, savePath, dpi);

            Doc theDoc = new Doc();
            theDoc.Read(filePath);
            theDoc.Rendering.DotsPerInch = dpi;
            count = theDoc.PageCount;

            for (int i = 1; i <= theDoc.PageCount; i++)
            {
                theDoc.PageNumber = i;
                theDoc.Rect.String = theDoc.CropBox.String;
                theDoc.Rendering.Save(savePath + i.ToString() + ".jpg");
            }
            theDoc.Clear();
        }

        public override Process getProcess()
        {
            foreach (var p in Process.GetProcesses())
            {
                if (p.ProcessName == process.ProcessName && p.Id != process.Id)
                    return p;
            }
            return null;
        }
    }
}
