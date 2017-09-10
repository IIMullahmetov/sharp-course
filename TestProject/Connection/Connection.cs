using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionLibrary
{
    public abstract class Сonnection
    {
        // Порт для комманд
        protected const int COMMAND_PORT = 0; //? сработает ли ?
        // Порт для звука
        protected const int SOUND_PORT = 0; //? сработает ли ?
        // Адрес сервера
        protected IPAddress ipAddress;
        // Локальная конечная точка
        protected IPEndPoint ipEndPoint;
        /*
        public int CommandPort
        {
            get { return COMMAND_PORT; }
        }

        public int SoundPort
        {
            get { return SOUND_PORT; }
        }
        */
        public abstract void CreateSocket();

        public abstract void Init();

        public abstract Task ReceiveAsync();

        public abstract Task SendAsync();

        public abstract void Shutdown();
    }
}
