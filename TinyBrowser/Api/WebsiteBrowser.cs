using System.IO;
using System.Net.Sockets;
using TinyBrowser.Api.Utility;

namespace TinyBrowser.Api{
    public class WebsiteBrowser : IWebsiteBrowser{
        public IWebPage GetWebPage(string host, int port){
            var tcpClient = new TcpClient(host, port);
            var stream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(stream){
                AutoFlush = true
            };
            streamWriter.Write($"GET / HTTP/1.1\r\nHost: {host}\r\n\r\n");
            var streamReader = new StreamReader(stream);
            var rawHtml = streamReader.ReadToEnd();
            tcpClient.Close();
            return rawHtml.ConvertHtmlToWebPage(host);
        }
    }
}