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

            string pathpp = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 8.pptx";
            PowerPointProgram presenter = new PowerPointProgram(pathpp, savePath);

            //string pathar = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 10.pdf";
            //AdobeReaderProgram presenter = new AdobeReaderProgram(pathar, savePath);

            ServerConnection cc = new ServerConnection();
            cc.Connection(savePath, presenter.getSlidesCount(), 1024, 4, presenter.getKeys());

            //cc.Connection(savePath, 86, 1024, 4);


            Console.ReadLine();
        }
    }
}
