using System;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
namespace Server
{
    static class Program
    {
        private static byte[] result = new byte[1024];
        private static byte[] client_result = new byte[1024];
        static Socket serverSocket;
        static TcpClient Client = new TcpClient();
        static IPAddress address = IPAddress.Parse("127.0.0.1");
        public static string msgSend = null;
        public static string msgIp = null;
        delegate void SetTextCallback(string text);
        static int port = 8007;
        public static int UserCount = 0;
        static string[] FastUsers = null;
        const string server_ip = "127.0.0.1";
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
                IPAddress address = IPAddress.Parse(server_ip);
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(address, port));
                serverSocket.Listen(10);
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
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                Thread receive_thread = new Thread(ReceiveMessage);
                receive_thread.Start(clientSocket);
            }

        }
 
        public static void sendMsg(string msg, string ip)
        {
            msgIp = ip;
            msgSend = msg;
        }
        public static void ReceiveMessage(object clientSocket)
        {

            Socket client_socket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    int receiveNumber = client_socket.Receive(result);
                    if (Encoding.ASCII.GetString(result, 0, receiveNumber) != null || Encoding.ASCII.GetString(result, 0, receiveNumber) != "")
                    {
                        string dataReceived = Encoding.ASCII.GetString(result, 0, receiveNumber);
                        if (dataReceived.Substring(0, 7) == "-Login>")
                        {
                            bool exist = false;
                            int x = 0;
                            for (x = 0; x != UserCount; x++)
                            {
                                if (FastUsers[x].Contains(LerEntre(dataReceived, "-Login>", '?')))
                                    exist = true;
                            }
                            if (exist == true)
                            {
                                string SenhaDigitada = LerEntre(dataReceived, "?Password=",'?');
                                var UsersIni = new FalaTu.IniClass(AppDomain.CurrentDomain.BaseDirectory + "/Config/Users.ini");
                                string SenhaOriginal = UsersIni.Read(LerEntre(dataReceived, "-Regist>", '?'), x.ToString());
                                if (SenhaDigitada == SenhaOriginal)
                                {
                                    string ip = LerEntre(dataReceived, "?ip=", ';');
                                    sendMsg("-Login>true?ID=" + x + "?", ip);
                                }
                                else
                                {
                                    string ip = LerEntre(dataReceived, "?ip=", ';');
                                    sendMsg("-Login>false", ip);
                                }
                            }
                            else
                            {
                                serverSocket.Send(Encoding.ASCII.GetBytes("Error"));
                            }
                        }
                        else if (dataReceived.Substring(0, 8) == "-Regist>")
                        {
                            int newUserID = UserCount + 1;
                            var UsersIni = new FalaTu.IniClass(AppDomain.CurrentDomain.BaseDirectory + "/Config/Users.ini");
                            UsersIni.Write("Qnt", newUserID.ToString(), "UserCount");
                            string ip = LerEntre(dataReceived, "?ip=", ';');
                            string Senha = LerEntre(dataReceived, "?Password=", '?');
                            UsersIni.Write(LerEntre(dataReceived, "-Regist>", '?'), Senha, newUserID.ToString());
                            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/Config/Users.frf", LerEntre(dataReceived, "-Regist>", '?') + ";" + newUserID);
                            WebClient wc = new WebClient();
                            wc.DownloadFile("https://i.imgur.com/EOVtYLr.png", AppDomain.CurrentDomain.BaseDirectory + "/Pictures/" + newUserID + ".jpg");
                            sendMsg("-Regist>true?ID=" + newUserID + ";", ip);
                            UserCount = UserCount + 1;
                        }
                        else if (dataReceived.Substring(0, 6) == "-Data>")
                        {
                            dataReceived = dataReceived.Substring(6);
                        }
                        else if (dataReceived.Substring(0, "-Picture>".Length) == "-Picture>")
                        {
                         
                        }
                        else
                        {

                        }
                    }


                }
                catch (Exception ex)
                {
                    break;
                }
            }
            
        }
        public static string LerEntre(string Completa,string Comeco, char Fim)
        {
            string getNameSub = Completa.Substring(Completa.IndexOf(Comeco) + Comeco.Length);
            string sa = getNameSub.Split(Fim)[0];
            return sa;
        }
    }
}
