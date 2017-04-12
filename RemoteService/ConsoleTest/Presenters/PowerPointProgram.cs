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

        public PowerPointProgram(string filePath, string savePath, int dpi)
        {
            base.keys = keys;
            Launch(processName, filePath);
            Rendering(filePath, savePath, dpi);
        }

        public override void Rendering(string filePath, string savePath, int dpi)
        {
            base.Rendering(filePath, savePath, dpi);

            try
            {
                oPowerPoint = new PowerPoint.Application(); //запускаем приложение
                oPres = oPowerPoint.Presentations; //презентации
                oPre = oPres.Open(filePath, Office.MsoTriState.msoFalse, Office.MsoTriState.msoFalse, Office.MsoTriState.msoFalse); //открываем презентацию
                oSlides = oPre.Slides; //считываем слайды
                count = oPre.Slides.Count;


                int width = dpi * 10;
                int height = Convert.ToInt32(dpi * 7.5);

                foreach (PowerPoint.Slide slide in oSlides)
                {
                    string p = slide.SlideIndex + ".jpg";
                    slide.Export(savePath + p, "JPG", width, height);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //oPre.Close(); //закрываем презентацию
                //oPowerPoint.Quit(); //выходим из программы
                Clean(); //чистим ресурсы
            }
        }

        public void Clean()
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

        public override Process getProcess()
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
    }
}
