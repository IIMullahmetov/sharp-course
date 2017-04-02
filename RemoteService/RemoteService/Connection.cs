using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteService
{
    class Connection
    {
        // Порт
        static int port = 1800;
        // Адрес
        static IPAddress ipAddress;
        // Локальная конечная точка
        IPEndPoint ipEndPoint;
        // Сокет
        Socket socket;
        //
        Socket handler;

        //КОМАНДЫ

        // Следующий слайд
        const byte codeNext = 1;
        // Предыдущий слайд
        const byte codePrev = 2;
        // Запуск презентации
        const byte codePlay = 3;
        // Выход
        const byte codeExit = 4;
        // Закрытие программы
        const byte codeClose = 5;

        public Connection()
        {
            setIPAddress();
            setIPEndPoint();
            setSocket();
            SendImage();
            ListenPort();
        }

        public void setIPAddress()
        {
            string host = Dns.GetHostName(); // получение имени компьютера
            string ip = Dns.GetHostEntry(host).AddressList[1].ToString(); // получение IP-адреса
            ipAddress = IPAddress.Parse(ip); //присваиваем IP-адрес
        }

        public void setIPEndPoint()
        {
            ipEndPoint = new IPEndPoint(ipAddress, port); // создаем локальную конечную точку
        }

        public void setSocket()
        {
            socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // создаем основной сокет

            try
            {
                socket.Bind(ipEndPoint); // связываем сокет с конечной точкой
                socket.Listen(1); // переходим в режим "прослушивания"

                handler = socket.Accept(); // ждем соединение, при удачном соединении создается новый экземпляр Socket
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Image getImageFromClipboard()
        {
            Image image = null;
            image = Clipboard.GetImage();
            image.Save("C:\\RemoteService\\Screen.png", ImageFormat.Png);
            return image;
        }

        private byte[] ImageToByteArray(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public void SendImage()
        {
            if (!Clipboard.ContainsImage())
            {
                SendKeys.SendWait("{PRTSC}");
            }
            byte[] data = ImageToByteArray(getImageFromClipboard());
            handler.Send(data);
        }

        public void ListenPort()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024]; // массив, где сохраняем принятые данные
                    int bytes = handler.Receive(data); //чтение данных
                    Console.WriteLine((data[0] - 48).ToString());
                    switch (data[0] - 48) // определяемся с командами клиента, 49 - это код символа в ASCII
                    {
                        case codeNext:   // следующий слайд                     
                            SendKeys.SendWait("{RIGHT}");
                            Thread.Sleep(100);
                            SendKeys.SendWait("{PRTSC}");
                            break;
                        case codePrev: // предыдущий слайд
                            SendKeys.SendWait("{LEFT}");
                            break;
                        case codePlay: // запуск презентации
                            SendKeys.SendWait("{F5}");
                            break;
                        case codeExit: // выход
                            SendKeys.SendWait("{ESC}");
                            break;
                        case codeClose: //закрытие программы
                            return;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Shutdown() // освобождаем сокеты
        {
            try
            {
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
