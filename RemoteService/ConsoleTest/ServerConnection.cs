using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleTest
{
    class ServerConnection
    {
        // Порт
        static int port = 1800;
        // Адрес
        static IPAddress ipAddress;
        // Локальная конечная точка
        IPEndPoint ipEndPoint;
        // Сокет
        Socket handler;
        //Название компьютера
        string host;
        //Клавиши
        string[] keys;

        //КОМАНДЫ

        // Следующий слайд
        const string codeNext = "-1";
        // Предыдущий слайд
        const string codePrev = "-2";
        // Запуск презентации
        const string codePlay = "-3";
        // Выход
        const string codeExit = "-4";
        // Закрытие программы
        const string codeClose = "-5";

        public async void Connection(string savePath, int slidesCount, int bufferLength, int codeBufferLength, string[] keys)
        {
            Configure(); 
            SetSocket();
            int sentSlidesCount = await AsyncSendImages(savePath, slidesCount, bufferLength);
            this.keys = keys;
            ListenPort(codeBufferLength);
        }

        public void Configure()
        {
            host = Dns.GetHostName(); // получение имени компьютера
            string ip = Dns.GetHostEntry(host).AddressList[1].ToString(); // получение IP-адреса // 1 для дома, 3 для универа
            ipAddress = IPAddress.Parse(ip); //присваиваем IP-адрес
            Console.WriteLine(ip);

            ipEndPoint = new IPEndPoint(ipAddress, port); // создаем локальную конечную точку
        }

        public void SetSocket()
        {
            Socket listenSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // создаем основной сокет

            listenSocket.Bind(ipEndPoint); // связываем сокет с конечной точкой

            listenSocket.Listen(1); // переходим в режим "прослушивания"

            handler = listenSocket.Accept(); //получаем подключение
        }

        public Task<int> AsyncSendImages(string savePath, int slidesCount, int bufferLength)
        {
            return Task.Run(() => SendImages(savePath, slidesCount, bufferLength));
        }

        public int SendImages(string savePath, int slidesCount, int bufferLength) //отправка картинок презентации
        {
            handler.Send(BitConverter.GetBytes(slidesCount)); //отправляем количество слайдов

            int i; // количество отправленных слайдов

            for (i = 1; i <= slidesCount; i++)
            {
                byte[] byteImage = ImageToByteArray(getImage(savePath, i)); //берем изображение и переводим в массив байт
                SendImages(byteImage, bufferLength); //отправка изображения
            }
            return i;
        }

        private Image getImage(string savePath, int index) //считываем изображение
        {
            return Image.FromFile(savePath + index + ".jpg");
        }

        private byte[] ImageToByteArray(Image img) //конвертируем картинку в массив байт
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public void SendImages(byte[] byteData, int bufferLength) //отправка данных (изображения)
        {
            Console.WriteLine("Размер - " + byteData.Length);

            int meta = handler.Send(BitConverter.GetBytes(byteData.Length)); //отправляем метаданные

            int sendBytes = 0; //общее количество отданных байт и рулетка в одном лице

            while (sendBytes < byteData.Length)
            {
                byte[] sendBuffer = new byte[byteData.Length - sendBytes >= bufferLength ? bufferLength : byteData.Length - sendBytes]; //буфер для отправки

                Array.Copy(byteData, sendBytes, sendBuffer, 0, sendBuffer.Length); //сохраняем в буфер часть картинки

                sendBytes += sendBuffer.Length; //сдвигаем индекс

                handler.Send(sendBuffer); //отправка данных
            }

            Console.WriteLine("Отправлено - " + sendBytes);
        }

        public async void ListenPort(int bufferLength)
        {
            try
            {
                while (true)
                {
                    byte[] receiveBuffer = new byte[bufferLength];

                    handler.Receive(receiveBuffer);

                    string result = await AsyncParseAndSendCode(receiveBuffer);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally //освобождаем сокеты
            {
               handler.Shutdown(SocketShutdown.Both);
               handler.Close();
            }
        }

        public Task<string> AsyncParseAndSendCode(byte[] receiveBuffer)
        {
            return Task.Run(() => ParseAndSendCode(receiveBuffer));
        }

        public string ParseAndSendCode(byte[] receiveBuffer)
        {
            string code = ParseCommand(receiveBuffer);
            SendCode(code);
            return code;
        }

        public string ParseCommand(byte[] receiveBuffer)
        {
            string code = Encoding.Unicode.GetString(receiveBuffer);

            Console.WriteLine(code);

            switch (code) // определяемся с командами клиента, 49 - это код символа в ASCII
            {
                case codeNext:
                    SendKeys.SendWait(keys[0]);
                    break;
                case codePrev:
                    SendKeys.SendWait(keys[1]);
                    break;
                case codePlay: // запуск презентации
                    SendKeys.SendWait(keys[2]);
                    break;
                case codeExit: // выход
                    SendKeys.SendWait(keys[3]);
                    break;
                case codeClose: //закрытие программы
                    break;
                default: //переход к слайду
                    string sendCode = code + keys[4];
                    SendKeys.SendWait(sendCode);
                    break;
            }
            return code;
        }

        public void SendCode(string code) //отправка данных (команды)
        {
            byte[] sendBuffer = Encoding.Unicode.GetBytes(code); //буфер для отправки

            handler.Send(sendBuffer); //отправка данных
        }

/*
                public void FirstReceive() // принимаем конфигурационные данные устройства
                {
                    byte[] receiveBuffer = new byte[1024];

                    handler.Receive(receiveBuffer);

                    Console.WriteLine(Encoding.Unicode.GetString(receiveBuffer));
                }

                public void FirstSend() //отправляем данные для устройства (например, название компа для сохранения)
                {
                    byte[] sendBuffer = Encoding.Unicode.GetBytes(host);

                    handler.Send(sendBuffer);
                }
        */
        /*
                        internal static bool Waiting(IAsyncResult ar)
                        {
                            int i = 0;
                            while (ar.IsCompleted == false)
                            {
                                if (i++ > 40)
                                {
                                    Console.WriteLine("Timed out.");
                                    return false;
                                }
                                Console.Write(".");
                                Thread.Sleep(500);
                            }
                            return true;
                        }
        */
    }
}
