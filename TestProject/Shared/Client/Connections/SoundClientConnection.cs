using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shared.Client.Connections;

namespace Shared.Client.Connections
{
    public class SoundClientConnection : ClientConnection
    {
        public override void CreateSocket()
        {
            throw new NotImplementedException();
        }

        public override void Init()
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
