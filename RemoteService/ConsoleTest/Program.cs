using ConsoleTest.Presenters;
using System;
using System.Threading;

//OpenDocumentPresentation читается PowerPoint'ом с 2007 SP2 версии!

namespace ConsoleTest
{
    class Program
    {
        private static Presenter presenter;
        private static ServerConnection cc;
        private static ManualResetEvent createEvent;
        private const int imageBufferLength = 1024;
        private const int metaBufferLength = 4;

        static void Main(string[] args)
        {
            FirstConnect();
            StartNewPresentation(null, null); // <= у тебя здесь пути

            Console.ReadLine();
        }

        static void FirstConnect()
        {
            cc = new ServerConnection();
            createEvent = new ManualResetEvent(true);
            ThreadPool.QueueUserWorkItem((StartListening) => { cc.Connection(createEvent, imageBufferLength, metaBufferLength); });
        }

        static void StartNewPresentation(string parfilePath, string parsavePath) // <= здесь типа приходит путь для новой презентации
        {
            /*
            string filePath = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 16.pdf";
            presenter = new AdobeReaderProgram(createEvent, savePath, ".jpg", 96);
            */

            string filePath = "C:\\Users\\" + Environment.UserName + "\\GoogleDrive\\Учеба\\Информатика\\Лекция 8.pptx";
            string savePath = "C:\\RemoteService\\Pictures\\"; // <= можно дать выбрать путь сохранения

            presenter = new PowerPointProgram(createEvent, savePath, ".jpg", 96);
            presenter.LaunchNewPresentation(filePath); // <= в параметры приходит путь к презентации
            cc.SendNewPresentation(presenter, imageBufferLength, metaBufferLength);
        }
    }
}
