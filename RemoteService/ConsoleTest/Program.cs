using ConsoleTest.Presenters;
using System;

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

            
            string filePath = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 8.pptx";
            Presenter presenter = new PowerPointProgram(filePath, savePath, ".jpg", 96);
            
            /*
            string filePath = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 16.pdf";
            Presenter presenter = new AdobeReaderProgram(filePath, savePath, ".jpg", 96);
            */

            ServerConnection cc = new ServerConnection();
            cc.Connection(presenter, imageBufferLength, metaBufferLength);

            Console.ReadLine();
        }
    }
}
