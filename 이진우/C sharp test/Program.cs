using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace C_sharp_test
{
    class Program
    {
        static void Main(string[] args)
        {
            string data;
            string input;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newsock.Bind(ipep);
            newsock.Listen(10);
            Console.WriteLine("waiting for a client...");

            Socket client = newsock.Accept();
            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;

            Console.WriteLine("Conected with {0} at port {1}", clientep.Address, clientep.Port);
        }
    }
}