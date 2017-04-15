using System.IO;
using Xamarin.Forms;

namespace MobileApp
{
    class ClientImageConverter
    {
        public static ImageSource ByteArrayToImage(byte[] byteImage)
        {
            return ImageSource.FromStream(() => new MemoryStream(byteImage)); //превращаем массив байт в изображение
        }
    }
}
