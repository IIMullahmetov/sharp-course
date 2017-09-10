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
        private static int saveIndex;

        public static Image GetImage(ManualResetEvent createEvent, string savePath, string extension, int index) //считываем изображение
        {
            string filePath = savePath + index + extension;
            if (saveIndex < index)
                createEvent.WaitOne();
            Image img = Image.FromFile(filePath);
            Bitmap btm = new Bitmap(img);
            img.Dispose();
            return btm;
        }

        public static byte[] ImageToByteArray(Image img) //конвертируем картинку в массив байт
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static void SetSaveIndex(int i) //индекс сохраненного слайда
        {
            saveIndex = i;
        }
    }
}