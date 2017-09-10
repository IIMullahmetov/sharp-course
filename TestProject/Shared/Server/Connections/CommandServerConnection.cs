using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Server.Connections
{
    public class CommandServerConnection : ServerConnection
    {
        // Событие для создания изображения
        private ManualResetEvent createEvent;
        // Событие для отправки изображения
        private ManualResetEvent sendEvent;
        // Количество клиентов
        private int CLIENTS_COUNT = 1;

        public CommandServerConnection(ManualResetEvent createEvent) : base()
        {
            this.createEvent = createEvent;
            sendEvent = new ManualResetEvent(false);
        }

        public override void CreateSocket()
        {
            Socket listenSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // создаем основной сокет
            listenSocket.Bind(ipEndPoint); // связываем сокет с конечной точкой
            listenSocket.Listen(CLIENTS_COUNT); // переходим в режим "прослушивания"
            handler = listenSocket.Accept(); //получаем подключение
            sendEvent.Set();
        }

        public override Task ReceiveAsync()
        {
            throw new NotImplementedException();
        }

        public override Task SendAsync()
        {
            throw new NotImplementedException();
        }

        public override void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
