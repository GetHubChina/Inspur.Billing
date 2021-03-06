﻿using NLog;
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
        /// 日志对象
        /// </summary>
        Logger _logger = LogManager.GetCurrentClassLogger();
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
                _logger.Info("Socket连接成功");
            }
        }
        byte[] buffer;
        /// <summary>
        /// 异步接受收数据
        /// </summary>
        public void ReciveAsync()
        {
            buffer = new byte[1024];
            _socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReciveCallback), null);
        }
        /// <summary>
        /// 同步接收数据
        /// </summary>
        /// <returns></returns>
        public MessageModel Recive()
        {
            buffer = new byte[1024];
            int count = _socket.Receive(buffer, 0, buffer.Length, 0);
            if (count > 0)
            {
                byte[] results = new byte[count];
                Array.Copy(buffer, 0, results, 0, count);
                MessageModel messageModel = _tcpData.Decode(results);
                if (messageModel.MessageId == 0)
                {
                    return Recive();
                }
                else
                {
                    return messageModel;
                }
            }
            return new MessageModel();
        }
        private void ReciveCallback(IAsyncResult ar)
        {
            try
            {
                if (_socket == null)
                {
                    return;
                }
                int end = _socket.EndReceive(ar);
                if (end > 0)
                {
                    byte[] data = new byte[end];
                    Array.Copy(buffer, 0, data, 0, end);
                    _logger.Info(string.Format("接收消息字节 {0}", string.Join(" ", data.ToArray())));
                    MessageModel messageModel = _tcpData.Decode(data);
                    if (messageModel.MessageId == 0)
                    {
                        _socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReciveCallback), null);
                    }
                    else
                    {
                        Complated(this, messageModel);
                        _logger.Info(string.Format("接收消息 {0}", messageModel.Message));
                    }
                }
            }
            catch (SocketException ex)
            {
                _logger.Error(ex.Message + ex.StackTrace);
            }
        }

        public void Send(byte id, string data)
        {
            _socket.Send(_tcpData.Encode(id, data));
        }
        public void Close()
        {
            _socket.Close();
            _logger.Info("关闭Socket");
            _socket = null;
        }

        public event EventHandler<MessageModel> Complated;

        public bool IsConnected
        {
            get { return _socket.Connected; }
        }
    }
}
