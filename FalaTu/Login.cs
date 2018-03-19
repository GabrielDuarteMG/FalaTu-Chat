using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FalaTu
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        TcpClient Client = new TcpClient();
        IPAddress address = IPAddress.Parse("127.0.0.1");
        int port = 8007;

        private void Form1_Load(object sender, EventArgs e)
        {
            Client.Connect(address, port);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream stream;
            string msg = "-Login>" + textBox1.Text + "?Password=" + textBox2.Text;
            stream = Client.GetStream();
            byte[] by = Encoding.UTF8.GetBytes(msg.ToCharArray(), 0, msg.Length);
            stream.Write(by, 0, by.Length);
            stream.Flush();
            NetworkStream nwStream = Client.GetStream();
            byte[] buffer = new byte[Client.ReceiveBufferSize];

            int bytesRead = nwStream.Read(buffer, 0, Client.ReceiveBufferSize);
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            nwStream.Write(buffer, 0, bytesRead);
            if (dataReceived == "error")
                MessageBox.Show("Deu");
        }
    }
}
