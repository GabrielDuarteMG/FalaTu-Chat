using System;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
namespace Server
{
    class Program
    {
        static int port = 8007;
        public static int UserCount = 0;
        static string[] FastUsers = null;
        const string server_ip = "127.0.0.1";
        public static IPAddress andress = IPAddress.Parse(server_ip);
        public static TcpListener serverSocket = new TcpListener(andress, port);
        [STAThread]
        static void Main(string[] args)
        {
            if (!Directory.Exists("Config"))
            {
                DialogResult InstalarYN = MessageBox.Show("Deseja instalar o servidor FalaTu Chat?", "Instalação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (InstalarYN == DialogResult.Yes)
                {
                    Directory.CreateDirectory("Config");
                    Directory.CreateDirectory("Logs");
                    Directory.CreateDirectory("Pictures");
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + "/Config/Users.frf");
                    var ConfigIni = new FalaTu.IniClass(AppDomain.CurrentDomain.BaseDirectory  + "/Config/Config.ini");
                    var UsersIni = new FalaTu.IniClass(AppDomain.CurrentDomain.BaseDirectory + "/Config/Users.ini");
                    UsersIni.Write("Qnt", "0", "UserCount");
                    Application.Restart();
                }
                else
                    return;
            }
            else
            {
                var UsersIni = new FalaTu.IniClass(AppDomain.CurrentDomain.BaseDirectory + "/Config/Users.ini");
                string QntString = UsersIni.Read("Qnt", "UserCount");
                Int32.TryParse(QntString, out UserCount);
                FastUsers = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "/Config/Users.frf");
                WebClient wc = new WebClient();
                serverSocket.Start();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\nServidor FalaTu Chat - Online");
                Console.WriteLine("IP do servidor= " + wc.DownloadString(new Uri("http://myip.dnsdynamic.org/")) + ":" + port.ToString());
                Console.WriteLine("<!======================Log Servidor========================!>");
                //IF YOU WANT RUN THIS SERVER IN BACKGROUND, COMMENT THIS CODE IN LINE 18 EVEN LINE 29(Remove comment from line 31 until line 33)
                //TcpClient client = serverSocket.AcceptTcpClient();
                //while (true)
                //{
                //    NetworkStream nwStream = client.GetStream();
                //    byte[] buffer = new byte[client.ReceiveBufferSize];

                //    int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                //    Console.WriteLine(dataReceived);
                //    nwStream.Write(buffer, 0, bytesRead);
                //}
                //RUN IN BACKGROUND CASE YOUR PROGRAM CONTAINS USER INTERFACE
                Thread server_thread = new Thread(Method);
                server_thread.IsBackground = true;
                server_thread.Start();
                FalaTu_Servidor.Server_Hud ServerHud = new FalaTu_Servidor.Server_Hud();
                ServerHud.ShowDialog();
                return;
            }

        }
        static void Method()
        {
            TcpClient cliente = serverSocket.AcceptTcpClient();
            ListenClientConnect(cliente);

        }
        public static string LerEntre(string Completa,string Comeco, char Fim)
        {
            string getNameSub = Completa.Substring(Completa.IndexOf(Comeco) + Comeco.Length);
            string sa = getNameSub.Split(Fim)[0];
            return sa;
        }
        static void ListenClientConnect(TcpClient client)
        {
            while (true)
            {
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                nwStream.Write(buffer, 0, bytesRead);
                if (dataReceived.Substring(0, 7) == "-Login>")
                {
                    bool exist = false;
                    int x = 0;
                    for (x =0; x != UserCount; x++)
                    {
                        if (FastUsers[x].Contains(LerEntre(dataReceived, "-Login>", '?')))
                            exist = true;
                    }
                    if(exist == true)
                    {
                        string SenhaDigitada = dataReceived.Substring(dataReceived.IndexOf("?Password=") + "?Password=".Length);
                        var UsersIni = new FalaTu.IniClass(AppDomain.CurrentDomain.BaseDirectory + "/Config/Users.ini");
                        string SenhaOriginal = UsersIni.Read(LerEntre(dataReceived, "-Regist>", '?'), x.ToString());
                        if (SenhaDigitada == SenhaOriginal)
                            MessageBox.Show("Conectou");
                    }
                    else
                    {
                        Stream stream;
                        string msg = "Error";
                        stream = client.GetStream();
                        byte[] by = Encoding.UTF8.GetBytes(msg.ToCharArray(), 0, msg.Length);
                        stream.Write(by, 0, by.Length);
                        stream.Flush();
                    }
                }
                else if(dataReceived.Substring(0, 8) == "-Regist>")
                {
                    int newUserID = UserCount + 1; 
                    var UsersIni = new FalaTu.IniClass(AppDomain.CurrentDomain.BaseDirectory + "/Config/Users.ini");
                    UsersIni.Write("Qnt", newUserID.ToString(), "UserCount");
                    string Senha = dataReceived.Substring(dataReceived.IndexOf("?Password=") + "?Password=".Length);
                    UsersIni.Write(LerEntre(dataReceived, "-Regist>",'?'), Senha, newUserID.ToString());
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/Config/Users.frf", LerEntre(dataReceived, "-Regist>", '?') + ";" + newUserID);
                    UserCount = UserCount + 1;
                }
                else if (dataReceived.Substring(0, 6) == "-Data>")
                {
                    dataReceived = dataReceived.Substring(6);
                }
                else if (dataReceived.Substring(0, 7) == "-Close>")
                {
                    client.Close();                    
                }
                else
                    Console.Write(dataReceived);
            }
        }
    }
}
