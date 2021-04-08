using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServer
{
    class Program
    {
        public static void Main()
        {
            int bytes;
            byte[] data = new byte[256];
            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, 9050);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            int m;
            try
            {
                socket.Bind(ipLocal);

                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint remoteIP = (EndPoint)(sender);
                while (true)
                {
                    data = new byte[256];
                    bytes = socket.ReceiveFrom(data, ref remoteIP);

                    string temp = Encoding.UTF8.GetString(data, 0, bytes);
                    if (temp == "exit") break;
                    Console.WriteLine($"ПРИНЯТО ({remoteIP.ToString() + "||" + DateTime.Now.ToString()}):\n {temp}");

                    if (temp.Length % 5 == 0)
                        temp = temp.Count(x => x == '{' || x == '}' || x == '[' || x == ']' || x == '(' || x == ')').ToString();
                    else
                        temp = $"Длина({temp.Length}) строка \"{temp}\" НЕ кратно 5!";

                    Console.WriteLine($"ОТПРАВЛЕНО ({DateTime.Now.ToString()}):\n {temp}");
                    data = Encoding.UTF8.GetBytes(temp);
                    socket.SendTo(data, data.Length, SocketFlags.None, remoteIP);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }
    }
}
