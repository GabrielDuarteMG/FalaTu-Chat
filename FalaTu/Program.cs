using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FalaTu
{
    static class Program
    {
        private static byte[] result = new byte[1024];
        private static byte[] client_result = new byte[1024];
        public static string myIp;
        private static int port = 8007;
        public static Socket clientSocket;
        const string server_ip = "127.0.0.1";
        public static IPAddress andress = IPAddress.Parse(server_ip);
        public static TcpListener server_Socket = new TcpListener(andress, 8008);
        public static string ServerResposta = null;
        [STAThread]
        static void Main()
        {
            IPAddress target_ip = IPAddress.Parse("127.0.0.1");
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                WebClient wc = new WebClient();
                myIp = wc.DownloadString(new Uri("http://myip.dnsdynamic.org/"));
                clientSocket.Connect(new IPEndPoint(target_ip, port));
                server_Socket.Start();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Login());
         

            }
            catch (Exception ex)
            {
                return;
            }
          
        }
        public static void ListenServerResponse()
        {
    
            TcpClient cliente = server_Socket.AcceptTcpClient();
            ReceiveServerMessage(cliente);
        }
        public static void ReceiveServerMessage(TcpClient client)
        {
            while (true)
            {
                try
                {
                    NetworkStream nwStream = client.GetStream();
                    byte[] buffer = new byte[client.ReceiveBufferSize];

                    int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                    MessageBox.Show(Encoding.ASCII.GetString(buffer, 0, bytesRead));
                    ServerResposta = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    nwStream.Write(buffer, 0, bytesRead);
                }
                catch
                {

                }
            }
        }
    }
}
