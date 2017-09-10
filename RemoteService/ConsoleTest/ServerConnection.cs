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
        private const int port = 1800;
        // Адрес
        private static IPAddress ipAddress;
        // Локальная конечная точка
        private IPEndPoint ipEndPoint;
        // Сокет
        private Socket handler;
        //Объект презентации
        private Presenter presenter;
        //
        private ManualResetEvent createEvent;
        private ManualResetEvent sendEvent;

        public void Connection(ManualResetEvent createEvent, int imageBufferLength, int metaBufferLength)
        {
            this.createEvent = createEvent;
            sendEvent = new ManualResetEvent(false);
            Configure();
            SetSocket();
            ListenPort(metaBufferLength);
        }

        public void SendNewPresentation(Presenter presenter, int imageBufferLength, int metaBufferLength)
        {
            this.presenter = presenter;
            NewThreadRenderingImages();
            NewThreadSendDataAndImages(imageBufferLength);
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
            sendEvent.Set();
        }

        public void NewThreadRenderingImages()
        {
            ThreadPool.QueueUserWorkItem((RenderingImages) => {
                try
                {
                    sendEvent.WaitOne();
                    presenter.SavePagesRendering(handler);
                }
                catch (SocketException) { return; }
            });
        }

        public void NewThreadSendDataAndImages(int imageBufferLength)
        {
            ThreadPool.QueueUserWorkItem((SendData) => {
                try
                {
                    sendEvent.WaitOne();
                    sendEvent.Reset();
                    SendPresentationData(presenter.GetSlidesCount()); //отправляем сведения о презентации
                    for (int i = 1; i <= presenter.GetSlidesCount(); i++)
                    {
                        Console.WriteLine(i);
                        byte[] byteImage = ServerImageConverter.ImageToByteArray(ServerImageConverter.GetImage(createEvent, presenter.GetSavePath(), presenter.GetExtension(), i)); //берем изображение и переводим в массив байт
                        sendEvent.Reset();
                        SendImage(byteImage, imageBufferLength); //отправка изображения
                        sendEvent.Set();
                    }
                }
                catch (SocketException) { return; }
                catch (ObjectDisposedException) { return; }
                finally { presenter.Clear(); }
            });
        }

        public void SendPresentationData(int slidesCount)
        {
            handler.Send(BitConverter.GetBytes(presenter.GetPresentationName().Length)); //отправляем метаданные названия презентации
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
                //ВОТ ЗДЕСЬ УВЕДОМЛЕНИЕ О РАЗРЫВЕ СВЯЗИ ИЛИ ОТКЛЮЧЕНИИ
                Console.WriteLine(e.Message);
            }
            finally { Shutdown(); }
        }

        public Task AsyncParseAndSendCode(byte[] receiveBuffer)
        {
            return Task.Run(() => {
                int response;
                if (KeySend.ParseCommand(presenter, receiveBuffer))
                    response = -1;
                else
                    response = -2;
                sendEvent.WaitOne();
                SendResponse(response);
            });
        }

        public void SendResponse(int response) //отправка ответа (команды)
        {
            try
            {
                byte[] sendBuffer = BitConverter.GetBytes(response); //буфер для отправки
                handler.Send(sendBuffer); //отправка данных
            }
            catch (SocketException) { return; }
            catch (ObjectDisposedException) { return; }
        }

        public void Shutdown()
        {
            try
            {
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                presenter.DeleteDirectory();
            }
            catch (ObjectDisposedException) { return; }
        }
    }
}