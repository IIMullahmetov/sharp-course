using ConsoleTest.Presenters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class ServerImageConverter
    {
        Presenter presenter;
        string savePath;
        ManualResetEvent createEvent;
        string filePath;

        public ServerImageConverter(Presenter presenter, ManualResetEvent createEvent)
        {
            this.presenter = presenter;
            savePath = presenter.GetSavePath();
            this.createEvent = createEvent;
        }

        public Image GetImage(int index) //считываем изображение
        {
            filePath = savePath + index + presenter.GetExtension();
            while (!ExistsImage(filePath))
                createEvent.WaitOne();
            Thread.Sleep(50);
            return Image.FromFile(filePath);
        }

        private bool ExistsImage(string filePath)
        {
            Console.WriteLine("СЕЙЧАС - " + DateTime.Now);
            Console.WriteLine("ВРЕМЯ ЗАПИСИ - " + File.GetLastWriteTime(filePath));
            if (File.Exists(filePath))
            {
                return true;
            }
            return false;
        }

        private bool FileIsBeingUsed(string fileName)
        {
            try
            {
                File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
            }

            catch (Exception exp)
            {
                return true;
            }
            return false;
        }

        public byte[] ImageToByteArray(Image img) //конвертируем картинку в массив байт
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
