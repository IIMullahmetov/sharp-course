using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Server.Connections
{
    public class SoundServerConnection : ServerConnection
    {
        public override void CreateSocket()
        {
            throw new NotImplementedException();
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
