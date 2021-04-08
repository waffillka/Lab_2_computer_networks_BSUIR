using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UDPClient
{

    class Program
    {
        public static void Main()
        {
            byte[] data = new byte[256];
            string temp;

            string address = "127.0.0.1";

            UdpClient server = new UdpClient(address, 9050);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("{0}", sender.ToString());

            while (true)
            {
                data = new byte[256];
                Console.Write("Введите сообщение: ");
                temp = Console.ReadLine();
                Console.WriteLine($"ОТПРАВЛЕНО {DateTime.Now.ToString()}:\n {temp}");
                data = Encoding.UTF8.GetBytes(temp);

                server.Send(data, data.Length);

                if (temp == "exit") break;

                data = server.Receive(ref sender);
                Console.WriteLine($"ПРИНЯТО {sender.ToString() + "||" + DateTime.Now.ToString()}:");
                temp = Encoding.UTF8.GetString(data, 0, data.Length);
                Console.WriteLine(temp + "\n");
            }
            Console.WriteLine("Остановка клиента...");
            server.Close();
        }

    }
}
