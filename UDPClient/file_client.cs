using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPClient
{
    class FileClient
    {


        const int BUFSIZE = 1024;
        const int PORT = 9000;
        const string ServerIP = "10.0.0.1";
        private FileClient(string[] args) {
            
                   
            
            while(true){

                Console.WriteLine("Enter l or L to get the server loadaverage");
                Console.WriteLine("Enter u og U to get the server uptime");


                byte[] CommandBytes = ReadLineAndConvert();

                
                UdpClient Client = new UdpClient(PORT);
                IPEndPoint ClientEnd = new IPEndPoint(IPAddress.Parse(ServerIP), PORT);

                Client.Connect(ClientEnd);
                Client.Send(CommandBytes, CommandBytes.Length);
				string RecievedString = RecieveByteAndConvert(Client, ClientEnd);
                Client.Close();                
                
                Console.WriteLine(RecievedString);
            }

        }
        

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting up ...");
            new FileClient(args);
        }
        
        private byte[] ReadLineAndConvert(){
            string CommandString = Console.ReadLine();
            byte[] CommandBytes = Encoding.ASCII.GetBytes(CommandString);
            
            return CommandBytes;
        }
        
        
        private string RecieveByteAndConvert(UdpClient server, IPEndPoint serverEnd){
            byte[] RecievedBytes = server.Receive(ref serverEnd);
            string RecievedString = Encoding.ASCII.GetString(RecievedBytes);

            return RecievedString;
        }
    }
}
