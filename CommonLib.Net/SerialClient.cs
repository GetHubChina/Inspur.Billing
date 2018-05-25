using NLog;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommonLib.Net
{
    public class SerialClient
    {
        #region 字段
        /// <summary>
        /// 串行端口通讯对象
        /// </summary>
        SerialPort _serialPort = new SerialPort();

        /// <summary>
        /// 日志对象
        /// </summary>
        Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 默认编码方式
        /// </summary>
        Encoding _defaultEncoding = Encoding.UTF8;

        /// <summary>
        /// 包头第一个字节
        /// </summary>
        const byte HEADER1 = 0x1A;
        /// <summary>
        /// 包头第二个字节
        /// </summary>
        const byte HEADER2 = 0x5D;

        //一次未读取完剩余的字节
        List<byte> _unreadBuffer = new List<byte>();
        /// <summary>
        /// 正文之前的头长度
        /// </summary>
        const int HEADLENGTH = 7;
        /// <summary>
        /// RCR长度
        /// </summary>
        const int RCRLENGTH = 2;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="portName">串口名</param>
        /// <param name="baudRates">波特率</param>
        /// <param name="parity">奇偶校验位</param>
        /// <param name="dataBits">数据长度</param>
        /// <param name="stopBits">停止位</param>
        public SerialClient(string portName, int baudRates, Parity parity, int dataBits, StopBits stopBits)
        {
            _serialPort.PortName = portName;
            _serialPort.BaudRate = baudRates;
            _serialPort.Parity = parity;
            _serialPort.DataBits = dataBits;
            _serialPort.StopBits = stopBits;

            _serialPort.ReadTimeout = 30000;
            _serialPort.WriteTimeout = 30000;
            _serialPort.DataReceived += _serialPort_DataReceived;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            _serialPort.Open();
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        public void Send(byte id, string message)
        {
            byte[] sendBytes = GetSendBytes(id, message);
            _serialPort.Write(sendBytes, 0, sendBytes.Count());
        }
        private byte[] GetSendBytes(byte id, string message)
        {
            List<byte> sendBytes = new List<byte>();
            sendBytes.Add(0x1A);
            sendBytes.Add(0x5D);
            sendBytes.Add(id);
            byte[] data = Encoding.UTF8.GetBytes(message);
            //写入内容长度
            byte[] lengthBytes = BitConverter.GetBytes(data.Length);
            //写入报文的时候高位在前，低位在后
            Array.Reverse(lengthBytes);
            sendBytes.AddRange(lengthBytes);
            sendBytes.AddRange(data);

            byte[] bytes = sendBytes.ToArray();
            ushort crc = CalculationCrc(bytes, bytes.Count());

            byte[] crcBytes = BitConverter.GetBytes(crc);
            Array.Reverse(crcBytes);
            sendBytes.AddRange(crcBytes);
            _logger.Info(string.Format("串口数据发送 ,内容：{0}，编码成字节数据：{1}", message, BitConverter.ToString(sendBytes.ToArray())));
            return sendBytes.ToArray();
        }
        /// <summary>
        /// 计算校验码
        /// </summary>
        private ushort CalculationCrc(byte[] data, int length)
        {
            ushort i;
            uint crc = 0;

            foreach (var item in data)
            {
                for (i = 0x80; i != 0; i /= 2)
                {
                    if ((crc & 0x8000) != 0)
                    {
                        crc *= 2;
                        crc ^= 0x18005;
                    }
                    else
                    {
                        crc *= 2;
                    }
                    if ((item & i) != 0)
                        crc ^= 0x18005;
                }
            }
            Console.WriteLine((ushort)crc);
            return (ushort)crc;
        }

        #endregion

        #region 事件
        public event EventHandler<MessageModel> Complated;

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] bytes = new byte[_serialPort.ReadBufferSize];
                int length = _serialPort.Read(bytes, 0, bytes.Length);
                if (length > 0)
                {
                    byte[] data = new byte[length];
                    Array.Copy(bytes, 0, data, 0, length);
                    _logger.Info(string.Format("接收消息字节 {0}", BitConverter.ToString(data.ToArray())));
                    MessageModel messageModel = _tcpData.Decode(data);
                    if (messageModel.MessageId == 0)
                    {
                    }
                    else
                    {
                        //接收数据成功之后，解除程序的阻塞
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Complated(this, messageModel);
                        }));
                        _logger.Info(string.Format("接收消息 {0}", messageModel.Message));
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Info(string.Format("_serialPort_DataReceived ,内容：{0}，位置：{1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion
    }
}
