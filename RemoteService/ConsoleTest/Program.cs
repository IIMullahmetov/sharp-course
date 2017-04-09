using ConsoleTest.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//ОПРЕДЕЛИТЬ ОПТИМАЛЬНОЕ КОЛИЧЕСТВО ТОЧЕК НА ДЮЙМ У ИЗОБРАЖЕНИЯ --- ПОКА ОСТАВИЛ НА 96 DPI
//OpenDocumentPresentation читается PowerPoint'ом с 2007 SP2 версии!

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string savePath = "C:\\RemoteService\\Pictures\\";
            string[] keys = { "{RIGHT}", "{LEFT}", "^(l)", "{ESC}", "^(+n)" };
            
            string pathpp = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 8.pptx";
            PowerPointProgram presenter = new PowerPointProgram(pathpp, savePath, 96);
            
            /*
            string pathar = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 16.pdf";
            AdobeReaderProgram presenter = new AdobeReaderProgram(pathar, savePath, 96);
            */
            ServerConnection cc = new ServerConnection();
            cc.Connection(savePath, presenter.getSlidesCount(), 1024, 4, presenter.getKeys());
            
            //cc.Connection(savePath, 72, 1024, 4, keys);


            Console.ReadLine();
        }
    }
}
