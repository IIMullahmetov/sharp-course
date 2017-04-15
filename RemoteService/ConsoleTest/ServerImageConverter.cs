using ConsoleTest.Presenters;
using System.Drawing;

namespace ConsoleTest
{
    class ServerImageConverter
    {
        public static Image GetImage(Presenter presenter, string savePath, int index) //считываем изображение
        {
            return Image.FromFile(savePath + index + presenter.GetExtension());
        }

        public static byte[] ImageToByteArray(Image img) //конвертируем картинку в массив байт
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
