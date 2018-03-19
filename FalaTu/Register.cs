using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing;
using System.Collections.Specialized;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.RegularExpressions;
namespace FalaTu
{
    public partial class Register : Form
    {
        public Form Login;
        public string PicPath;
        public Register()
        {
            InitializeComponent();
        }

        public Register(Form LoginForm)
        {
            InitializeComponent();
            Login = LoginForm;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            TcpClient Client = new TcpClient();
            IPAddress address = IPAddress.Parse("127.0.0.1");
            int port = 8007;
            Client.Connect(address, port);
            Stream stream;
            string msg = "-Regist>" + textBox1.Text + "?Password=" + textBox2.Text + "?ip=" + Program.myIp + ";";
            stream = Client.GetStream();
            byte[] by = Encoding.UTF8.GetBytes(msg.ToCharArray(), 0, msg.Length);
            stream.Write(by, 0, by.Length);
            stream.Flush();
            Application.Restart();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Login.Show();
        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        
    }
}
