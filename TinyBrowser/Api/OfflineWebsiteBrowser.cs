using System;
using System.IO;
using TinyBrowser.Api.Utility;

namespace TinyBrowser.Api{
    public class OfflineWebsiteBrowser : IWebsiteBrowser{
        static readonly string Path = Environment.CurrentDirectory;
        public IWebPage GetWebPage(string host, int port){

            try{
                var rawHtml = File.ReadAllText($"{Path}/{host}.Html");
                return rawHtml.ConvertHtmlToWebPage(host);
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}