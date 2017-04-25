﻿using System;
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
        PowerPoint.Application oPowerPoint = null;
        PowerPoint.Presentations oPres = null;
        PowerPoint.Presentation oPre = null;
        PowerPoint.Slides oSlides = null;

        string processName = "POWERPNT";
        new string programName = "PowerPoint";
        new string presentationWindowName = "Демонстрация PowerPoint";
        new string[] keys = { "{RIGHT}", "{LEFT}", "{F5}", "{ESC}", "~" };

        private int width;
        private int height;

        public PowerPointProgram(ManualResetEvent createEvent, string filePath, string savePath, string extension, int dpi)
        {
            base.createEvent = createEvent;
            base.keys = keys;
            base.extension = extension;
            base.programName = programName;
            base.presentationWindowName = presentationWindowName;
            presentationName = Path.GetFileNameWithoutExtension(filePath);
            format = extension.Substring(1, extension.Length - 1);
            DeleteDirectory(savePath);
            Launch(processName, filePath);
            CreateDirectory(savePath);
            Configure(filePath, dpi);
        }

        public override void Configure(string filePath, int dpi)
        {
            width = dpi * 10;
            height = Convert.ToInt32(dpi * 7.5);

            oPowerPoint = new PowerPoint.Application(); //запускаем приложение
            oPres = oPowerPoint.Presentations; //презентации
            oPre = oPres.Open(filePath, Office.MsoTriState.msoFalse, Office.MsoTriState.msoFalse, Office.MsoTriState.msoFalse); //открываем презентацию
            oSlides = oPre.Slides; //считываем слайды
            count = oPre.Slides.Count;
        }

        public override void SavePageRendering(int index)
        {
            oPre.Slides[index].Export(savePath + index + extension, format, width, height);
        }

        public override void SavePagesRendering(Socket handler)
        {
            for (int i = 1; i <= GetSlidesCount(); i++)
            {
                createEvent.Reset();
                if (handler == null) return; //ВОТ ЗДЕСЬ, АЛЬМЮСЛИ
                oPre.Slides[i].Export(savePath + i + extension, format, width, height);
                createEvent.Set();
            }
        }

        public override string GetCommandGoPage(int code)
        {
            return code + GetKey(4);
        }

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
