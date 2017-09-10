using System;
using System.Threading.Tasks;

namespace Shared.Client.Connections
{
    public class CommandClientConnection : ClientConnection
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
