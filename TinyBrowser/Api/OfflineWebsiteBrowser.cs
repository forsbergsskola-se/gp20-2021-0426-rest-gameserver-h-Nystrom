using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TinyBrowser.Api.Utility;

namespace TinyBrowser.Api{
    public class OfflineWebsiteBrowser : WebsiteBrowser, IWebsiteBrowser{
        static readonly string Path = Environment.CurrentDirectory;
        
        protected override string GetWebPageHtml(string host, int port){
            return File.ReadAllText($"{Path}/{host}.Html");
        }
        
    }
}