using ConsoleTest.Presenters;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        //Объект презентации
        Presenter presenter;

        public void Connection(Presenter presenter, int imageBufferLength, int metaBufferLength)
        {
            this.presenter = presenter;
            Configure();
            SetSocket();
            NewThreadSendImages(imageBufferLength);
            ListenPort(metaBufferLength);
        }

        public void Configure()
        {
            string host = Dns.GetHostName(); // получение имени компьютера
            string ip = Dns.GetHostEntry(host).AddressList[4].ToString(); // получение IP-адреса // 1 для дома, 3 для универа
            ipAddress = IPAddress.Parse(ip); //присваиваем IP-адрес 
            ipEndPoint = new IPEndPoint(ipAddress, port); // создаем локальную конечную точку

            Console.WriteLine(ip);
        }

        public void SetSocket()
        {
            Socket listenSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // создаем основной сокет
            listenSocket.Bind(ipEndPoint); // связываем сокет с конечной точкой
            listenSocket.Listen(1); // переходим в режим "прослушивания"
            handler = listenSocket.Accept(); //получаем подключение
        }

        public void NewThreadSendImages(int imageBufferLength)
        {
            var data = new ImagesData(presenter.GetSavePath(), presenter.GetSlidesCount(), imageBufferLength);
            ThreadPool.QueueUserWorkItem(new WaitCallback(SendData), data);
        }

        public void SendData(object obj)
        {
            var data = (ImagesData)obj;
            SendPresentationData(data.slidesCount); //отправляем сведения о презентации
            for (int i = 1; i <= data.slidesCount; i++)
            {
                Console.WriteLine(i);

                presenter.SavePageRendering(i);
                byte[] byteImage = ServerImageConverter.ImageToByteArray(ServerImageConverter.GetImage(presenter, data.savePath, i)); //берем изображение и переводим в массив байт
                SendImage(byteImage, data.imageBufferLength); //отправка изображения
            }
            presenter.Clear();
        }

        public void SendPresentationData(int slidesCount)
        {
            handler.Send(BitConverter.GetBytes(presenter.GetPresentationName().Length)); //отправляем метаданные
            handler.Send(Encoding.Unicode.GetBytes(presenter.GetPresentationName())); //отправляем название презентации
            handler.Send(BitConverter.GetBytes(slidesCount)); //отправляем количество слайдов
        }

        public void SendImage(byte[] byteData, int bufferLength) //отправка данных (изображения)
        {
            Console.WriteLine("Размер - " + byteData.Length);
            handler.Send(BitConverter.GetBytes(byteData.Length)); //отправляем метаданные
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

        public async void ListenPort(int metaBufferLength)
        {
            try
            {
                while (true)
                {
                    byte[] receiveBuffer = new byte[metaBufferLength];
                    handler.Receive(receiveBuffer);
                    await AsyncParseAndSendCode(receiveBuffer);
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
            return Task.Run(() =>
            {
                string response;
                Console.WriteLine("\nServerTask - " + Encoding.Unicode.GetString(receiveBuffer) + "\n");
                if (KeySend.ParseCommand(presenter, receiveBuffer))
                    response = "-1";
                else
                    response = "-2";
                SendResponse(response);
                return response;
            });
        }

        public void SendResponse(string response) //отправка данных (команды)
        {
            byte[] sendBuffer = Encoding.Unicode.GetBytes(response); //буфер для отправки
            handler.Send(sendBuffer); //отправка данных
        }
    }
}
