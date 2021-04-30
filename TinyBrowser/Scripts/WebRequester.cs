using System.IO;
using System.Net.Sockets;
using TinyBrowser.Scripts.Data;

namespace TinyBrowser.Scripts{
    public struct WebRequester{
       public static RequestData HttpRequest(string host, int port){
            var tcpClient = new TcpClient(host, port);
            var stream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(stream){
                AutoFlush = true
            };
            streamWriter.Write($"GET / HTTP/1.1\r\nHost: {host}\r\n\r\n");
            
            var streamReader = new StreamReader(stream);
            var result = streamReader.ReadToEnd();
            tcpClient.Close();
            return new RequestData(result, host);
        }
    }
}