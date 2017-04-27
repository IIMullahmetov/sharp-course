using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using Office = Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace ConsoleTest.Presenters
{
    class PowerPointProgram : Presenter
    {
        private PowerPoint.Application oPowerPoint = null;
        private PowerPoint.Presentations oPres = null;
        private PowerPoint.Presentation oPre = null;
        private PowerPoint.Slides oSlides = null;

        private int width;
        private int height;

        public PowerPointProgram(ManualResetEvent createEvent, string presentationPath, string savePath, string extension, int dpi)
            : base(createEvent, presentationPath, savePath, extension, dpi)
        {
            SetParametres();
            ConfigureRendering();
            Launch();
        }

        public void SetParametres()
        {
            keys = new string[] { "{RIGHT}", "{LEFT}", "{F5}", "{ESC}", "%(+{F4})", "~" };
            programName = "PowerPoint";
            presentationWindowName = "Демонстрация PowerPoint";
            processName = "POWERPNT";
        }

        public override void ConfigureRendering()
        {
            try
            {
                width = dpi * 10;
                height = Convert.ToInt32(dpi * 7.5);

                oPowerPoint = new PowerPoint.Application(); //запускаем приложение
                oPres = oPowerPoint.Presentations; //презентации
                oPre = oPres.Open(presentationPath, Office.MsoTriState.msoFalse, Office.MsoTriState.msoFalse, Office.MsoTriState.msoFalse); //открываем презентацию
                oSlides = oPre.Slides; //считываем слайды
                count = oPre.Slides.Count;
            }
            catch (DirectoryNotFoundException)
            {
                //УВЕДОМЛЕНИЕ О ТОМ, ЧТО ПУТЬ УКАЗАН НЕВЕРНО
                return;
            }
        }

        public override void SavePageRendering(int index)
        {
            oPre.Slides[index].Export(savePath + index + extension, format, width, height);
        }

        public override void SavePagesRendering(Socket handler)
        {
            for (int i = 1; i <= GetSlidesCount(); i++)
            {
                if (handler.Connected == false)  //ВОТ ЗДЕСЬ, АЛЬМЮСЛИ
                    return;
                createEvent.Reset();
                oPre.Slides[i].Export(savePath + i + extension, format, width, height);
                ServerImageConverter.SetIndex(i);
                createEvent.Set();
            }
        }

        public override string GetCommandGoPage(int code)
        {
            return code + GetKey(5);
        }
/*
        public override void SetProcessId()
        {
            if (process.HasExited)
            {
                foreach (var p in Process.GetProcesses())
                {
                    if (p.ProcessName == processName)
                    {
                        processId = p.Id;
                        //process = p;
                        break;
                    }
                }
            }
            else
                processId = process.Id;
        }
*/
        public override void Clear()
        {
            if (oSlides != null)
            {
                Marshal.FinalReleaseComObject(oSlides);
                oSlides = null;
            }
            if (oPre != null)
            {
                Marshal.FinalReleaseComObject(oPre);
                oPre = null;
            }
            if (oPres != null)
            {
                Marshal.FinalReleaseComObject(oPres);
                oPres = null;
            }
            if (oPowerPoint != null)
            {
                Marshal.FinalReleaseComObject(oPowerPoint);
                oPowerPoint = null;
            }
        }
    }
}
