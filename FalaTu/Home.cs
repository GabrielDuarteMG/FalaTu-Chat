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
    public partial class Home : Form
    {
        public static string ID;
        public static string Usuario;
        public static Image FotoDePerfil;
        public Home()
        {
            InitializeComponent();
        }
        public static string[] arguments = Environment.GetCommandLineArgs();
        static TcpClient Client = new TcpClient();
        static IPAddress address = IPAddress.Parse("127.0.0.1");
        static int port = 8007;
        private void Home_Load(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData("https://cdn3.iconfinder.com/data/icons/simple-files-1/128/Update-128.png");
            MemoryStream ms = new MemoryStream(bytes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            PessoasOnline.Items.Add(new testexListBox.exListBoxItem("Usuarios online", Environment.NewLine + "Click para atualizar", img));
            Client.Connect(address, port);
        }
        public void AddMsg(string msg)
        {
            testexListBox.exListBoxItem ObjMsg = new testexListBox.exListBoxItem(Usuario,msg,FotoDePerfil);
            ChatDeConversas.Items.Add(ObjMsg);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Stream stream;
            string msg = "-Data>" + textBox1.Text + "?UserID=" + ID;
            stream = Client.GetStream();
            byte[] by = Encoding.UTF8.GetBytes(msg.ToCharArray(), 0, msg.Length);
            stream.Write(by, 0, by.Length);
            stream.Flush();
            AddMsg(textBox1.Text);
            textBox1.Text = "";
        }
    }
}
