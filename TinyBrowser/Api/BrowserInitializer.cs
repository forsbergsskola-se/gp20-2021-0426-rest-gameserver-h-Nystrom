using System;

namespace TinyBrowser.Api{
    public struct BrowserInitializer{
        
        public const int Options = 3;
        public static IWebsiteBrowser GetBrowser(int playerInput){
            return playerInput switch{
                1 => new TcpClientWebsiteBrowser(),
                2 => new LocalWebsiteBrowser(),
                3 => new HttpWebRequestWebsiteBrowser(),
                _ => throw new ArgumentException("Accepted inputs are: 1 to 3")
            };
        }
    }
}