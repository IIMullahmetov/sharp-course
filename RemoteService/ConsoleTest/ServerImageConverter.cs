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
            createEvent.WaitOne();
            return Image.FromFile(filePath);
        }

        public byte[] ImageToByteArray(Image img) //конвертируем картинку в массив байт
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
