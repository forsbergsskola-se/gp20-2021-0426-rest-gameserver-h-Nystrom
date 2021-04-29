using System.IO;
using System.Net.Sockets;
using TinyBrowser.Scripts.Data;
using TinyBrowser.Scripts.Utility;

namespace TinyBrowser.Scripts{
    public struct WebRequester{
        public static WebPage SendAndReceiveRequest(string hostName, int port){
            var info = HttpRequest(hostName, port);
            var title = HtmlParser.GetTitle(info);
            var subPages = HtmlParser.GetSubPages(info);
            return new WebPage(info,title,subPages);
        }
        static string HttpRequest(string hostName, int port){
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