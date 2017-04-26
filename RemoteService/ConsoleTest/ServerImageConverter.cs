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
        static int saveIndex;

        public ServerImageConverter(Presenter presenter, ManualResetEvent createEvent)
        {
            this.presenter = presenter;
            savePath = presenter.GetSavePath();
            this.createEvent = createEvent;
        }

        public Image GetImage(int index) //считываем изображение
        {
            filePath = savePath + index + presenter.GetExtension();
            if (saveIndex < index)
                createEvent.WaitOne();
            Image img = Image.FromFile(filePath);
            Bitmap btm = new Bitmap(img);
            img.Dispose();
            return btm;
        }

        public byte[] ImageToByteArray(Image img) //конвертируем картинку в массив байт
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static void SetIndex(int i)
        {
            saveIndex = i;
        }
    }
}
