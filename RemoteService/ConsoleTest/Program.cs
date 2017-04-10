using ConsoleTest.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//OpenDocumentPresentation читается PowerPoint'ом с 2007 SP2 версии!

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int imageBufferLength = 1024;
            int codeBufferLength = 4;

            string savePath = "C:\\RemoteService\\Pictures\\";
            //string[] keys = { "{RIGHT}", "{LEFT}", "^(l)", "{ESC}", "^(+n)" };

            
            string filePath = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 8.pptx";
            Presenter presenter = new PowerPointProgram(filePath, savePath, 96);
            
            /*
            string filePath = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 16.pdf";
            Presenter presenter = new AdobeReaderProgram(filePath, savePath, 96);
            */

            ServerConnection cc = new ServerConnection();
            cc.Connection(savePath, presenter.getSlidesCount(), imageBufferLength, codeBufferLength, presenter.getKeys(), presenter.getProcess());
            
            //cc.Connection(savePath, 72, 1024, 4, keys);

            Console.ReadLine();
        }
    }
}
