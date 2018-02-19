using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Net
{
    public class SocketClient
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint endPoint = null;
        public SocketClient(string ip, int port)
        {
            IPAddress address = IPAddress.Parse(ip);
            endPoint = new IPEndPoint(address, port);
        }
        public void Connect()
        {
            if (!socket.Connected)
            {
                socket.Connect(endPoint);
            }
        }
        public void Recevied()
        {

        }
    }
}
