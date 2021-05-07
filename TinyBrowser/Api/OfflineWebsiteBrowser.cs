using System;
using System.IO;
using TinyBrowser.Api.Utility;

namespace TinyBrowser.Api{
    public class OfflineWebsiteBrowser : IWebsiteBrowser{
        static readonly string Path = Environment.CurrentDirectory;
        IWebPage webPage;
        public bool GetWebPage(string host, int port){
            try{
                var rawHtml = File.ReadAllText($"{Path}/{host}.Html");
                webPage = rawHtml.ConvertHtmlToWebPage(host);
                return true;
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void GetCurrentWebPage(){
            throw new NotImplementedException();
        }
    }
}