using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Net
{
    public class TcpHelper
    {
        /// <summary>
        /// 通讯的socket对象
        /// </summary>
        Socket _socket;
        /// <summary>
        /// 之定义的Tcp包数据
        /// </summary>
        TcpData _tcpData;

        public TcpHelper()
        {
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _tcpData = new TcpData(Encoding.ASCII);
        }
        public void Connect(IPAddress iPAddress, int port)
        {
            if (!_socket.Connected)
            {
                IPEndPoint point = new IPEndPoint(iPAddress, port);
                _socket.Connect(point);
                Console.WriteLine("连接成功。");
            }
        }
        byte[] buffer;
        public void Recive()
        {
            buffer = new byte[1024];
            _socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReciveCallback), null);
        }

        private void ReciveCallback(IAsyncResult ar)
        {
            try
            {
                int end = _socket.EndReceive(ar);
                if (end > 0)
                {
                    byte[] data = new byte[end];
                    Array.Copy(buffer, 0, data, 0, end);

                    MessageModel messageModel = _tcpData.Decode(data);
                    if (messageModel.MessageId == 0)
                    {
                        _socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReciveCallback), null);
                    }
                    else
                    {
                        Complated(this, messageModel);
                        Console.WriteLine(messageModel.Message);
                    }

                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Send(string data)
        {
            _socket.Send(_tcpData.Encode(0x01, data));
        }
        public void Close()
        {
            _socket.Close();
        }

        public event EventHandler<MessageModel> Complated;

        public bool IsConnected
        {
            get { return _socket.Connected; }
        }
    }
}
