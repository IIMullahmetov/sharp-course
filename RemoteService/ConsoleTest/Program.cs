using ConsoleTest.Presenters;
using System;
using System.Threading;

//OpenDocumentPresentation читается PowerPoint'ом с 2007 SP2 версии!

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int imageBufferLength = 1024;
            int metaBufferLength = 4;
            string savePath = "C:\\RemoteService\\Pictures\\";
            ManualResetEvent createEvent = new ManualResetEvent(false);

            string filePath = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 8.pptx";
            Presenter presenter = new PowerPointProgram(createEvent, filePath, savePath, ".jpg", 96);

            /*
            string filePath = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 16.pdf";
            Presenter presenter = new AdobeReaderProgram(createEvent, filePath, savePath, ".jpg", 96);
            */

            ServerConnection cc = new ServerConnection();
            cc.Connection(presenter, createEvent, imageBufferLength, metaBufferLength);
            Console.ReadLine();
        }
    }
}
