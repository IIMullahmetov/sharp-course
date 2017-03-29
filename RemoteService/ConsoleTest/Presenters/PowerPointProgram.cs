using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Office = Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace ConsoleTest.Presenters
{
    class PowerPointProgram
    {
        PowerPoint.Application oPowerPoint = null;
        PowerPoint.Presentations oPres = null;
        PowerPoint.Presentation oPre = null;
        PowerPoint.Slides oSlides = null;

        string[] keys = { "{RIGHT}", "{LEFT}", "{F5}", "{ESC}", "~"};

        int count;

        public PowerPointProgram(string path, string savePath)
        {
            Launch(path, savePath);
        }

        public void Launch(string path, string savePath)
        {
            try
            {
                oPowerPoint = new PowerPoint.Application(); //запускаем приложение
                oPres = oPowerPoint.Presentations; //презентации
                oPre = oPres.Open(path, Office.MsoTriState.msoFalse, Office.MsoTriState.msoFalse, Office.MsoTriState.msoTrue); //открываем презентацию
                oSlides = oPre.Slides; //считываем слайды
                count = oPre.Slides.Count;

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                foreach (PowerPoint.Slide slide in oSlides) {
                    string p = slide.SlideIndex + ".jpg";
                    slide.Export(savePath + p, "JPG", 0, 0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                oPre.Close(); //закрываем презентацию
                oPowerPoint.Quit(); //выходим из программы
                Clean(); //чистим ресурсы
            }
        }

        public int getSlidesCount()
        {
            return count;
        }

        public string[] getKeys()
        {
            return keys;
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
    }
}
