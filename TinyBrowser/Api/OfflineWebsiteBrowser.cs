using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TinyBrowser.Api.Utility;

namespace TinyBrowser.Api{
    public class OfflineWebsiteBrowser : WebsiteBrowser, IWebsiteBrowser{
        static readonly string Path = Environment.CurrentDirectory;
        
        protected override string GetWebPageHtml(string host, int port){
            try{
                return File.ReadAllText($"{Path}/{host}.Html");
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                throw;
            }
        }
        
    }
}