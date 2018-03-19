using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FalaTu
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread response_thread = new Thread(Program.ListenServerResponse);
            response_thread.IsBackground = true;
            response_thread.Start();
        }
        public static string LerEntre(string Completa, string Comeco, char Fim)
        {
            string getNameSub = Completa.Substring(Completa.IndexOf(Comeco) + Comeco.Length);
            string sa = getNameSub.Split(Fim)[0];
            return sa;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FalaTu.Program.clientSocket.Send(Encoding.ASCII.GetBytes("-Login>" + textBox1.Text + "?Password=" + textBox2.Text + "?ip=" + Program.myIp + ";"));
            while(Program.ServerResposta == null){ }
            if (LerEntre( Program.ServerResposta, "-Login>",'?') == "true")
            {
                Home.ID = LerEntre(Program.ServerResposta, "?ID=", ';');
                Home homeLogin = new Home();
                Home.Usuario = textBox1.Text;
                homeLogin.Show();
                this.Hide();
            }else if (Program.ServerResposta == "-Login>false")
            {
                MessageBox.Show("Credenciais incorretas","FalaTu Chat",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Restart();
            }
            Program.ServerResposta = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register RegisterWindow = new Register(this);
            RegisterWindow.Show();
            this.Hide();
        }
    }
}
