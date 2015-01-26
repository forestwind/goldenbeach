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
            //네트워크의 끝점을 IP번호와 포트번호를 설정


            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newsock.Bind(ipep);
            //bind()
            newsock.Listen(10);
            //listen()
            Console.WriteLine("waiting for a client...");

            Socket client = newsock.Accept();
            //accept
            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
            //네트워크의 끝점을 가져옴

            Console.WriteLine("Conected with {0} at port {1}", clientep.Address, clientep.Port);

            NetworkStream ns = new NetworkStream(client);
            //client의 네트워크 끝점을 가져옴

            //연결(데이터를 보내고 받는 메서드 제공)
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            string welcome = "Welcome to my test";
            //지정된 데이터에 라인 피드값 추가하여 해당 스트림 전송
            sw.WriteLine(welcome);
            sw.Flush(); 
            //사용한 스트림을 비워줌


            sw.WriteLine(clientep.Address);
            sw.Flush();

            sw.WriteLine(clientep.Port);
            sw.Flush();


            while(true)
            {
                try
                {
                    data = sr.ReadLine(); // client에서 받은 string을 data에 담습니다.
                    Console.WriteLine("Client Receive {0}-{1} << {2} ", clientep.Address, clientep.Port, data);
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e);
                }
                Console.Write("Client Send {0}-{1} >>", ipep.Address, ipep.Port);

                input = Console.ReadLine(); //client에서 받은 string을 data에 담습니다.
                if (input == "exit") break;


                //client에서 받은 input가 exit라면 while문 탈출
                sw.WriteLine(input); //client에서 받았던 input data를 다시 보냄
                sw.Flush();

            }

            Console.WriteLine("Disconnect from {0}", clientep.Address);
            ns.Close();
            sw.Close(); //stream을 닫음
            ns.Close();
            client.Shutdown(SocketShutdown.Both); //소켓 비활성화
            client.Close(); //소켓닫기

        }
    }
}