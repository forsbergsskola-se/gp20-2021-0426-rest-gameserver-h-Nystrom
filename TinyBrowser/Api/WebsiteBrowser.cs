using System;
using System.IO;
using System.Net.Sockets;
using TinyBrowser.Api.Utility;

namespace TinyBrowser.Api{
    public class WebsiteBrowser : IWebsiteBrowser{
        IWebPage webPage;
        public bool GetWebPage(string host, int port){
            var tcpClient = new TcpClient(host, port);
            var stream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(stream){
                AutoFlush = true
            };
            streamWriter.Write($"GET / HTTP/1.1\r\nHost: {host}\r\n\r\n");
            var streamReader = new StreamReader(stream);
            var rawHtml = streamReader.ReadToEnd();
            tcpClient.Close();
            webPage = rawHtml.ConvertHtmlToWebPage(host);
            return true;
        }

        public void GetCurrentWebPage(){
            throw new NotImplementedException();
        }
    }
}