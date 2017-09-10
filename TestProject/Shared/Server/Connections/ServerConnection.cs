using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Shared.Server.Interfaces;

namespace Shared.Server.Connections
{
    public abstract class ServerConnection : Сonnection
    {
        // Сокет для комманд
        protected Socket handler;
        // Сокет для звука
        protected Socket talker;
        /*
        public override void Init() //НЕ ВИДИТ БИБЛИОТЕКИ
        {
            string host = Dns.GetHostName(); // получение имени компьютера
            string ip = Dns.GetHostEntry(host).AddressList[4].ToString(); // получение IP-адреса // 1 для дома, 3 для универа
            ipAddress = IPAddress.Parse(ip); //присваиваем IP-адрес 
            ipEndPoint = new IPEndPoint(ipAddress, port); // создаем локальную конечную точку

            Console.WriteLine(ip);
        }
        */
        // СПОСОБ 1 - точно и умно, но есть зависимость от DNS-сервера и нужно обработать при отсутствии подключения
        public override void Init()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                ipEndPoint = socket.LocalEndPoint as IPEndPoint;
            }
            ipAddress = ipEndPoint.Address;
            ipEndPoint = new IPEndPoint(ipAddress, COMMAND_PORT);

            Console.WriteLine(ipAddress.ToString());
        }
    }
}
