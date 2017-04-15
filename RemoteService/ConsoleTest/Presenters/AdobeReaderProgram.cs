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
        Doc theDoc;

        string processName = "AcroRd32";

        new string[] keys = { "{RIGHT}", "{LEFT}", "^(l)", "{ESC}", "^(+n)", "~" };

        public AdobeReaderProgram(string filePath, string savePath, string extension, int dpi)
        {
            base.keys = keys;
            base.extension = extension;
            Launch(processName, filePath);
            CreateDirectory(savePath);
            Configure(filePath, dpi);
            SetProcess();
        }

        public override void Configure(string filePath, int dpi)
        {
            theDoc = new Doc();
            theDoc.Read(filePath);
            theDoc.Rendering.DotsPerInch = dpi;
            count = theDoc.PageCount;
        }

        public override void SavePageRendering(int index)
        {
            theDoc.PageNumber = index;
            theDoc.Rect.String = theDoc.CropBox.String;
            theDoc.Rendering.Save(savePath + index.ToString() + extension);
        }

        public override string GetCommandGoPage(string code)
        {
            return GetKey(4) + code + GetKey(5);
        }

        public override void SetProcess()
        {
            foreach (var p in Process.GetProcesses())
            {
                if (p.ProcessName == process.ProcessName && p.Id != process.Id)
                {
                    process = p;
                    break;
                }
            }
        }

        public override void Clear() { theDoc.Clear(); }
    }
}
