using System;

namespace TinyBrowser.Api{
    public class BrowserInitializer{

        public static IWebsiteBrowser GetBrowser(int playerInput){
            switch (playerInput){
                    case 1:
                        return new OnlineWebsiteBrowser();
                    case 2:
                        return new OfflineWebsiteBrowser();
            }
            throw new ArgumentException("Accepted inputs are: 1 or 2");
        }
    }
}