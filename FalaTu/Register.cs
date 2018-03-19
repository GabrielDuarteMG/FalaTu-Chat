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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TcpClient Client = new TcpClient();
            IPAddress address = IPAddress.Parse("127.0.0.1");
            int port = 8007;
            Client.Connect(address, port);
            Stream stream;
            string msg = "-Regist>" + textBox1.Text + "?Password=" + textBox2.Text;
            stream = Client.GetStream();
            byte[] by = Encoding.UTF8.GetBytes(msg.ToCharArray(), 0, msg.Length);
            stream.Write(by, 0, by.Length);
            stream.Flush();
        }
    }
}
