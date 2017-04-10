using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

//!!!ПЕРЕД ЗАВЕРШЕНИЕМ РАБОТЫ НУЖНО ВЫЗЫВАТЬ Shutdown() - закрытие сокетов

namespace MobileApp
{
    class ClientConnection
    {
        // Порт
        static int port = 1800;
        // Адрес
        static IPAddress ipAddress;
        // Локальная конечная точка
        IPEndPoint ipEndPoint;
        // Сокет
        Socket socket;
        //Размер буфера для изображений
        int imageBufferLength;
        //Размер буфера для кода
        int codeBufferLength;

        public ClientConnection(int imageBufferLength, int codeBufferLength)
        {
            this.imageBufferLength = imageBufferLength;
            this.codeBufferLength = codeBufferLength;
        }

        public Task<List<ImageSource>> Connection(object message)
        {
            return Task.Run(() =>
            {
                SetIPAddress((string)message);
                Configure();
                return GetReceiveImages(codeBufferLength, imageBufferLength);
            });
        }

        public void SetIPAddress(string IP)
        {
            ipAddress = IPAddress.Parse("2a02:2698:2822:4f08:7c0a:59ad:4e4f:bdc5"); //присваиваем IP-адрес
            //ipAddress = IPAddress.Parse(IP); //присваиваем IP-адрес
        }

        public void Configure()
        {
            ipEndPoint = new IPEndPoint(ipAddress, port); // создаем локальную конечную точку

            socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // создаем основной сокет

            socket.Connect(ipEndPoint);
        }

        public List<ImageSource> GetReceiveImages(int metaBufferLength, int bufferLength)
        {
            byte[] receiveMetaBuffer = new byte[metaBufferLength]; //буфер для количества слайдов

            socket.Receive(receiveMetaBuffer); //записываем метаданные

            int countBytes = BitConverter.ToInt32(receiveMetaBuffer, 0); //узнаем количество слайдов, которые нам придут

            List<ImageSource> images = new List<ImageSource>();

            for (int i = 1; i <= countBytes; i++)
            {
                images.Add(ByteArrayToImage(ReceiveImages(metaBufferLength, bufferLength)));
            }
            return images;
        }

        public byte[] ReceiveImages(int metaBufferLength, int bufferLength)
        {
            byte[] receiveMetaBuffer = new byte[metaBufferLength]; //массив для метаданных

            socket.Receive(receiveMetaBuffer); //записываем метаданные

            int countBytes = BitConverter.ToInt32(receiveMetaBuffer, 0); //узнаем количество байт, которые нам придут

            Console.WriteLine("countBytes - " + countBytes);

            byte[] byteArray = new byte[countBytes]; //создаем буфер для всей картинки

            int receiveBytes = 0; //общее количество принятых байт и рулетка в одном лице

            while (receiveBytes < countBytes)
            {
                byte[] receiveBuffer = new byte[countBytes - receiveBytes >= bufferLength ? bufferLength : countBytes - receiveBytes]; //буфер, куда записываем принятые данные (кусочек картинки)

                int bytes = socket.Receive(receiveBuffer); //записываем количество принятых байт

                receiveBuffer.CopyTo(byteArray, receiveBytes); //сохраняем принятые байты в хранилище

                receiveBytes += bytes; //сдвигаем индекс и суммируем общее количество принятых байт
            }

            Console.WriteLine("Пришло - " + receiveBytes);

            return byteArray;
        }

        public ImageSource ByteArrayToImage(byte[] byteImage)
        {
            return ImageSource.FromStream(() => new MemoryStream(byteImage)); //превращаем массив байт в изображение
        }

        public Task<int> Request(string message)
        {
            return Task.Run(() =>
            {
                SendCode(message);
                return ReceiveCode();
            });
        }

        public void SendCode(string message)
        {
            byte[] sendBuffer = Encoding.Unicode.GetBytes(message); // массив с данными

            socket.Send(sendBuffer);
        }

        //!!! Лучше чтобы размер приходил "сверху"

        public int ReceiveCode()
        {
            byte[] receiveBuffer = new byte[codeBufferLength];

            socket.Receive(receiveBuffer);

            return BitConverter.ToInt32(receiveBuffer, 0);
        }
        /*
                        public void Shutdown() // освобождаем сокеты
                        {
                            try
                            {
                                socket.Shutdown(SocketShutdown.Both);
                                socket.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
        */
    }
}
