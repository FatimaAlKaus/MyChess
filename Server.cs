using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ChessLib;
using System.Drawing;

namespace Library
{
    public class Server
    {
        public event ChessEventHandler Read;
        public event Action Connected;
        TcpListener tcpListener;
        TcpClient tcpClient;
        NetworkStream stream;
        async public void Write(object sender, ChessEventArgs e)
        {
            if (stream != null)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    binaryFormatter.Serialize(ms, e);

                    var buffer = ms.ToArray();
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                }
            }
        }
        public Server()
        {
            tcpListener = new TcpListener(IPAddress.Any, 8888);
        }
        async public void Listen()
        {
            tcpListener.Start();
            tcpClient = await tcpListener.AcceptTcpClientAsync();
            stream = tcpClient.GetStream();
            Connected?.Invoke();
            MessageBox.Show("Подключено!");

            await Task.Run(() =>
            {
                while (stream != null)
                {
                    try
                    {
                        byte[] data = new byte[1024];
                        stream.Read(data, 0, 1024);

                        BinaryFormatter bf = new BinaryFormatter();
                        ChessEventArgs eventArgs;
                        using (MemoryStream ms = new MemoryStream(data))
                        {
                            eventArgs = bf.Deserialize(ms) as ChessEventArgs;
                        }
                        Read?.Invoke(null, eventArgs);
                    }
                    catch
                    {
                        tcpListener.Stop();
                        MessageBox.Show("Ваш оппонент ливнул");
                        break;
                    }
                }
            });
        }
    }
}
