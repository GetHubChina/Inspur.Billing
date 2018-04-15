using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Net
{
    public class TcpData
    {
        #region 字段
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
        /// <summary>
        /// 消息标识
        /// </summary>
        byte _cmdId;
        /// <summary>
        /// 报文长度
        /// </summary>
        int _length;
        /// <summary>
        /// 校验码
        /// </summary>
        short _crc;

        //一次未读取完剩余的字节
        List<byte> _unreadBuffer = new List<byte>();
        /// <summary>
        /// 正文之前的头长度
        /// </summary>
        const int HEADLENGTH = 9;
        #endregion

        #region 构造函数
        public TcpData(Encoding encoding)
        {
            _defaultEncoding = encoding;
        }
        public TcpData() : this(Encoding.UTF8)
        {

        }
        #endregion

        #region 方法
        /// <summary>
        /// 自定义编码
        /// </summary>
        /// <param name="message"></param>
        public byte[] Encode(byte id, string message)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms, Encoding.UTF8))
                {
                    //写入头
                    bw.Write(HEADER1);
                    bw.Write(HEADER2);
                    //写入id
                    bw.Write(id);
                    //写入
                    byte[] data = _defaultEncoding.GetBytes(message);
                    //写入内容长度
                    bw.Write(BitConverter.GetBytes(data.Length));
                    //写入内容
                    bw.Write(data);
                    //写入校验码
                    List<byte> crcBytes = new List<byte>();
                    crcBytes.Add(HEADER1);
                    crcBytes.Add(HEADER1);
                    crcBytes.Add(id);
                    crcBytes.AddRange(BitConverter.GetBytes(data.Length));
                    crcBytes.AddRange(data);
                    bw.Write(CalculationCrc(crcBytes.ToArray(), crcBytes.Count));
                    return ms.ToArray();
                }
            }
        }
        /// <summary>
        /// 自定义解码
        /// </summary>
        /// <param name="data"></param>
        public MessageModel Decode(byte[] data)
        {
            //拷贝本次的有效字节  
            byte[] copyBuffer = new byte[data.Length];
            Array.Copy(data, 0, copyBuffer, 0, copyBuffer.Length);
            data = copyBuffer;
            if (this._unreadBuffer.Count > 0)
            {
                //拷贝之前遗留的字节  
                this._unreadBuffer.AddRange(copyBuffer);
                data = this._unreadBuffer.ToArray();
                this._unreadBuffer.Clear();
                this._unreadBuffer = new List<byte>();
            }

            MessageModel messageModel = new MessageModel();
            MemoryStream ms = new MemoryStream(data);
            BinaryReader br = new BinaryReader(ms, _defaultEncoding);
            try
            {
                byte[] buff;

                if (!LoopReadHeader(br))
                {
                    return messageModel;
                }

                #region 包协议  
                //读取消息id
                byte messageId = br.ReadByte();
                messageModel.MessageId = messageId;
                //读取报文长度
                int dataLength = br.ReadInt32();
                #endregion

                #region 包解析  
                //剩余字节数大于本次需要读取的字节数  
                if (dataLength + 2 <= (br.BaseStream.Length - br.BaseStream.Position))
                {
                    //读取内容
                    data = br.ReadBytes(dataLength);
                    messageModel.Data = data;
                    messageModel.Message = _defaultEncoding.GetString(data);
                    Console.WriteLine(messageModel.Message);
                    //读取校验码
                    short crc = br.ReadByte();
                    List<byte> crcList = new List<byte>();
                    crcList.Add(HEADER1);
                    crcList.Add(HEADER2);
                    crcList.Add(messageId);
                    crcList.AddRange(BitConverter.GetBytes(dataLength));
                    crcList.AddRange(data);
                    //将未读数据添加到未读字节列表
                    while (br.BaseStream.Position < br.BaseStream.Length - 1)
                    {
                        _unreadBuffer.Add(br.ReadByte());
                    }


                    if (crc != CalculationCrc(crcList.ToArray(), crcList.Count))
                    {
                        //crc 校验不通过
                        throw new Exception("CRC is invalid.");
                    }
                }
                else
                {
                    //剩余字节数刚好小于本次读取的字节数 存起来，等待接受剩余字节数一起解析  
                    buff = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position + 7));
                    _unreadBuffer.AddRange(buff);
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (br != null)
                {
                    br.Dispose();
                }
                br.Close();
                if (br != null)
                {
                    br.Dispose();
                }
                ms.Close();
                if (ms != null)
                {
                    ms.Dispose();
                }
            }
            return messageModel;
        }
        /// <summary>
        /// 计算校验码
        /// </summary>
        private short CalculationCrc(byte[] data, int length)
        {
            int i;
            int crc = 0;

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
            return (short)crc;
        }

        private bool LoopReadHeader(BinaryReader br)
        {
            //循环读取包头             
            //判断本次解析的字节是否满足常量字节数   
            if ((br.BaseStream.Length - br.BaseStream.Position) < HEADLENGTH)
            {
                byte[] _buff = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
                this._unreadBuffer.AddRange(_buff);
                return false;
            }
            byte header1 = br.ReadByte();
            byte header2 = br.ReadByte();
            if (!(HEADER1 == header1 && HEADER2 == header2))
            {
                br.BaseStream.Seek(-1, SeekOrigin.Current);
                return LoopReadHeader(br);
            }
            return true;
        }
        #endregion
    }
}
