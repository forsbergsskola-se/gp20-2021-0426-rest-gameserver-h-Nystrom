using System;
using System.IO;

namespace TinyBrowser.Api{
    public class OfflineWebsiteBrowser : WebsiteBrowser{
        static readonly string Path = Environment.CurrentDirectory;
        
        protected override string GetWebPageHtml(string host,string uri, int port){
            return File.ReadAllText($"{Path}/{host}.Html");
        }
        
    }
}