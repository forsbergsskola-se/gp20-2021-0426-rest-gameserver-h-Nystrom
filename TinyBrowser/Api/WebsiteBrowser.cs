using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using TinyBrowser.Api.Utility;

namespace TinyBrowser.Api{
    public class WebsiteBrowser : IWebsiteBrowser{
        IWebPage webPage;

        public int WebPageSubPageCount{ get; }
        public int WebPageHtmlCount{ get; }

        public bool CanReceiveWebPage(string host, int port){
            var tcpClient = new TcpClient(host, port);
            var stream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(stream){
                AutoFlush = true
            };
            streamWriter.Write($"GET / HTTP/1.1\r\nHost: {host}\r\n\r\n");
            var streamReader = new StreamReader(stream);
            var rawHtml = streamReader.ReadToEnd();
            tcpClient.Close();
            webPage = rawHtml.ConvertHtmlToWebPage();
            return true;
        }

        public string[] GetCurrentWebPage(){
            throw new NotImplementedException();
        }

        public bool TryGoBack(){
            throw new NotImplementedException();
        }

        public bool TryGoForward(){
            throw new NotImplementedException();
        }

        public bool TrGoToSubPage(string uri){
            throw new NotImplementedException();
        }

        public bool TryGoToHtmlIndex(int index){
            throw new NotImplementedException();
        }

        public void GetSearchHistory(){
            throw new NotImplementedException();
        }
    }
}