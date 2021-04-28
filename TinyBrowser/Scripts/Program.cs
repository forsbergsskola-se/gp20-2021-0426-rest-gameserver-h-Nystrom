using System;
using System.IO;
using System.Net.Sockets;

namespace TinyBrowser.Scripts{
    class Program{
        const string HostName = "acme.com";
        const int Port = 80;

        static void Main(string[] args){
            var websiteInfo = HttpWebsiteRequest(HostName, Port);
            Console.WriteLine(websiteInfo);
        }

        static string HttpWebsiteRequest(string hostName, int port){
            var tcpClient = new TcpClient(hostName, port);
            var stream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(stream){
                AutoFlush = true
            };
            streamWriter.Write($"GET / HTTP/1.1\r\nHost: {hostName}\r\n\r\n");
            
            var streamReader = new StreamReader(stream);
            var result = streamReader.ReadToEnd();
            tcpClient.Close();
            return result;
        }
    }
}
