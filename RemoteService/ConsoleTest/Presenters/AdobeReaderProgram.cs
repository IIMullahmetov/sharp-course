using System;
using System.Diagnostics;
using System.IO;
using WebSupergoo.ABCpdf10;

namespace ConsoleTest.Presenters
{
    class AdobeReaderProgram : Presenter
    {
        Doc theDoc;

        string processName = "AcroRd32";
        new string programName = "Adobe Acrobat Reader DC";
        new string[] keys = { "{RIGHT}", "{LEFT}", "^(l)", "{ESC}", "^(+n)", "~" };

        public AdobeReaderProgram(string filePath, string savePath, string extension, int dpi)
        {
            base.keys = keys;
            base.extension = extension;
            base.programName = programName;
            presentationName = Path.GetFileNameWithoutExtension(filePath);
            Launch(processName, filePath);
            CreateDirectory(savePath);
            Configure(filePath, dpi);
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

        public override void SetProcessId()
        {
            if (process.HasExited)
                SetOldProcess();
            else
                SetNewProcess();
        }

        private void SetOldProcess()
        {
            Process firstProcess = null;
            foreach (var p in Process.GetProcesses())
            {
                if (p.ProcessName == processName)
                {
                    if (firstProcess == null)
                        firstProcess = p;
                    else
                    {
                        if (firstProcess.StartTime > p.StartTime)
                        {
                            processId = firstProcess.Id;
                            //process = firstProcess;
                        }
                        else if (firstProcess.StartTime < p.StartTime)
                        {
                            processId = p.Id;
                            //process = p;
                        }
                        break;
                    }
                }
            }
        }

        private void SetNewProcess()
        {
            foreach (var p in Process.GetProcesses())
            {
                if (p.ProcessName == processName && process.Id != p.Id)
                {
                    processId = p.Id;
                    //process = p;
                    break;
                }
            }
        }

        public override void Clear() { theDoc.Clear(); }
    }
}
