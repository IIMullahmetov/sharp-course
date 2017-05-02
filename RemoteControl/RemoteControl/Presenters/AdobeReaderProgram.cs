using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleTest.Presenters
{
    class AdobeReaderProgram : Presenter
    {
        //private GhostscriptRasterizer rasterizer;

        public AdobeReaderProgram(ManualResetEvent createEvent, string presentationPath, string savePath, string extension, int dpi)
            : base(createEvent, presentationPath, savePath, extension, dpi)
        {
            SetParametres();
            ConfigureRendering();
            Launch();
        }

        public void SetParametres()
        {
            keys = new string[] { "{RIGHT}", "{LEFT}", "^(l)", "{ESC}", "%(+{F4})", "^(+n)", "~" };
            programName = "Adobe Acrobat Reader DC";
            processName = "AcroRd32";
        }

        public override void ConfigureRendering()
        {
            /*
            rasterizer = new GhostscriptRasterizer();
            rasterizer.Open(presentationPath);
            count = rasterizer.PageCount;
            */
        }

        public override void SavePageRendering(int index)
        {
            /*
            theDoc.PageNumber = index;
            theDoc.Rect.String = theDoc.CropBox.String;
            theDoc.Rendering.Save(savePath + index.ToString() + extension);
            */
        }

        public override void SavePagesRendering(Socket handler)
        {
            /*
            for (var i = 1; i <= GetSlidesCount(); i++)
            {
                var img = rasterizer.GetPage(dpi, dpi, i);
                img.Save(savePath + i + extension);
            }
            */
        }

        public override string GetCommandGoPage(int code)
        {
            return GetKey(5) + code + GetKey(6);
        }

        public override string GetCommandExitProgram()
        {
            return GetKey(4) + "{TAB}" + GetKey(6);
        }
        /*
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
        */
        public override void Clear() { /*rasterizer.Close();*/ }
    }
}
