using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessLib;
namespace Library
{
    public class Client
    {
        public event ChessEventHandler Read;
        public event Action Connected;
        TcpClient tcpClient;
        NetworkStream stream;
        async public void Connect()
        {
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 8888);
                stream = tcpClient.GetStream();
                Connected?.Invoke();
                MessageBox.Show("Подключились к серверу!");
                await Task.Run(() =>
                {
                    while (stream != null)
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
                });
            }
            catch (SocketException)
            {
                MessageBox.Show("Подключиться не удалось");
                return;
            }
            catch (Exception)
            {
                MessageBox.Show($"Ваш оппонент ливнул");
                tcpClient = null;
            }
        }
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
    }
}
