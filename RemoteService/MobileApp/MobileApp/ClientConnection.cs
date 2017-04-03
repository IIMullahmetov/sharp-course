using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

//!!! БЫСТРЫЕ МНОГОКРАТНЫЕ НАЖАТИЯ ЛОМАЕТ ПРОГРАММУ => МЕТАДАННЫЕ НЕ ВСЕГДА ПЕРЕДАЮТСЯ ПРАВИЛЬНО

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

        //КОМАНДЫ
        /*
        // Следующий слайд
        const byte codeNext = 1;
        // Предыдущий слайд
        const byte codePrev = 2;
        // Выход
        const byte codeExit = 3;
        // Закрытие программы
        const byte codeClose = 4;
        */
        public Task<List<ImageSource>> Connection(object message)
        {
            return Task.Run(() => getConnection((string)message));
        }

        public List<ImageSource> getConnection(string message)
        {
            SetIPAddress(message);
            Configure();
            return GetReceiveImages(4, 1024);
        }

        public void SetIPAddress(string IP)
        {
            //ipAddress = IPAddress.Parse("2a02:2698:2825:11a1:b18d:9818:1b32:4ca"); //присваиваем IP-адрес
            ipAddress = IPAddress.Parse(IP); //присваиваем IP-адрес
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
            return Task.Run(() => SendAndReceiveCode(message));
        }

        public int SendAndReceiveCode(string message)
        {
            SendCode(message);
            return ReceiveCode();
        }

        public void SendCode(string message)
        {
            byte[] sendBuffer = Encoding.Unicode.GetBytes(message); // массив с данными

            socket.Send(sendBuffer);
        }

        //!!! Лучше чтобы размер приходил "сверху"

        public int ReceiveCode()
        {
            byte[] receiveBuffer = new byte[4];

            socket.Receive(receiveBuffer);

            return BitConverter.ToInt32(receiveBuffer, 0);
        }

        /*
                public void FirstSend() //отправка конфигурационных данных
                {

                    string configuration = "Android 7.1.1";

                    byte[] sendBuffer = Encoding.Unicode.GetBytes(configuration); // массив с конфигурационными данными

                    socket.Send(sendBuffer);
                }
        */
        /*
                        internal static bool Waiting(IAsyncResult ar) //ожидание действия
                        {
                            int i = 0;
                            while (ar.IsCompleted == false)
                            {
                                if (i++ > 20)
                                {
                                    Console.WriteLine("Timed out.");
                                    return false;
                                }
                                Console.Write(".");
                                Thread.Sleep(100);
                            }
                            return true;
                        }
        */
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
