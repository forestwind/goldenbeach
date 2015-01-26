using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;

namespace C_sharp_test_client
{
    class Program
    {
        static void Main(string[] args)
        {
            string data;
            string input;
            string ipdata;
            string portdata;

            IPEndPoint ipep = new IPEndPoint( IPAddress.Parse("127.0.0.1"), 9050 );
            //네트워크 끝점을 ip번호와 포트번호를 할당
            Socket server = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

            try 
            {
                server.Connect(ipep);
            }
            catch (SocketException e)
            {
                Console.WriteLine("unable to connect to server.");
                Console.WriteLine(e.ToString());
                return;
            }

            NetworkStream ns = new NetworkStream(server);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            data = sr.ReadLine();
            Console.WriteLine(data);
            ipdata = sr.ReadLine();
            portdata = sr.ReadLine();

            Console.Write("Server Send {0}-{1}", ipdata, portdata);
            sw.Flush();

            while(true)
            {
                input = Console.ReadLine();
                if (input == "exit") break;

                sw.WriteLine(input);
                sw.Flush();

                data = sr.ReadLine();

                Console.WriteLine("Server Receive {0}-{1} << {2}", ipep.Address, ipep.Port, data);
                sw.Flush();

                Console.Write("Server Send {0}-{1} <<", ipdata, portdata);
                sw.Flush();

            }

            Console.WriteLine("Disconnect from Server");
            ns.Close();
            sw.Close(); //stream을 닫음
            ns.Close();
            server.Shutdown(SocketShutdown.Both); //소켓 비활성화
            server.Close(); //소켓닫기


        }
    }
}
