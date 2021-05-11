using System;

namespace TinyBrowser.Api{
    public struct BrowserInitializer{
        
        public const int Options = 3;
        public static IWebsiteBrowser GetBrowser(int playerInput){
            return playerInput switch{
                1 => new OnlineWebsiteBrowser(),
                2 => new OfflineWebsiteBrowser(),
                3 => new WebRequestWebsiteBrowser(),
                _ => throw new ArgumentException("Accepted inputs are: 1 to 3")
            };
        }
    }
}