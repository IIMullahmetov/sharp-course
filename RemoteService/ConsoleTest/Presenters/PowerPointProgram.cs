using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

        new string[] keys = { "{RIGHT}", "{LEFT}", "{F5}", "{ESC}", "~" };

        private int width;
        private int height;

        public PowerPointProgram(string filePath, string savePath, string format, int dpi)
        {
            base.keys = keys;
            base.format = format;
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
            oPre.Slides[index].Export(savePath + index + format, format.Substring(1, format.Length - 1), width, height);
        }

        public override string GetCommandGoPage(string code)
        {
            return code + GetKey(4);
        }

        public override Process GetProcess()
        {
            if (!process.HasExited)
                return process;
            else
                foreach (var p in Process.GetProcesses())
                {
                    if (p.ProcessName == processName)
                        return p;
                }
            return null;
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
