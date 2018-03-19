using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FalaTu_Servidor
{
    public partial class Server_Hud : Form
    {
        public Server_Hud()
        {
            InitializeComponent();
        }

        static TcpClient Client = new TcpClient();
        static IPAddress address = IPAddress.Parse("127.0.0.1");
        static int port = 8008;
        private void Server_Hud_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream stream;
            string msg = "ping";
            stream = Client.GetStream();
            byte[] by = Encoding.UTF8.GetBytes(msg.ToCharArray(), 0, msg.Length);
            stream.Write(by, 0, by.Length);
            stream.Flush();
        }

        private void button2_Click(object sender, EventArgs e)
        {
    
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        if(Server.Program.msgSend != null)
            {
                string msg = Server.Program.msgSend;
                IPAddress ads = IPAddress.Parse(Server.Program.msgIp);
                Server.Program.msgSend = null;
                Server.Program.msgIp = null;
                Client.Connect("127.0.0.1", 8008);
                Stream stream;
                stream = Client.GetStream();
                byte[] by = Encoding.UTF8.GetBytes(msg.ToCharArray(), 0, msg.Length);
                stream.Write(by, 0, by.Length);
                stream.Flush();
                Client = new TcpClient();   
            }
         }
    }
}
