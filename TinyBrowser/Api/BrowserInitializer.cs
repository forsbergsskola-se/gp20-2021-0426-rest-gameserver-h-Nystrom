using System;

namespace TinyBrowser.Api{
    public class BrowserInitializer{

        public static IWebsiteBrowser GetBrowser(ConsoleKey playerInput){
            switch (playerInput){
                    case ConsoleKey.D1:
                        return new OnlineWebsiteBrowser();
                    case ConsoleKey.D2:
                        return new OfflineWebsiteBrowser();
            }
            throw new ArgumentException("Accepted inputs are: 1 or 2");
        }
    }
}