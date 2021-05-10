using System;
using System.Globalization;
using TinyBrowser.Api;

namespace TinyBrowser{
    internal class Program{
        const string Host = "acme.com";
        const int Port = 80;

        static void Main(string[] args){
            var websiteBrowser = GetBrowserOption();

            if (!websiteBrowser.CanReceiveWebPage(Host, Port)){
                Console.WriteLine("Shutting down");
                return;
            }
            //TODO: Options: F forward, B back,G Goto html,S Goto subpage, escape exit, H history,
            
            
            var uriPages = websiteBrowser.GetCurrentWebPage();
            Console.ReadKey();
            websiteBrowser.TrGoToSubPage(uriPages[0]);
            websiteBrowser.GetCurrentWebPage();
            Console.ReadKey();
            websiteBrowser.TryGoBack();
            websiteBrowser.GetCurrentWebPage();
            Console.ReadKey();
            websiteBrowser.TrGoToSubPage(uriPages[6]);
            websiteBrowser.GetCurrentWebPage();
            Console.ReadKey();
            websiteBrowser.TrGoToSubPage(uriPages[8]);
            websiteBrowser.GetCurrentWebPage();
            Console.ReadKey();
            websiteBrowser.GetSearchHistory();//2st
            Console.ReadKey();
            websiteBrowser.TryGoBack();
            websiteBrowser.GetCurrentWebPage();
            Console.ReadKey();
            websiteBrowser.TryGoForward();
            websiteBrowser.GetCurrentWebPage();
            Console.ReadKey();
            websiteBrowser.TrGoToSubPage(uriPages[2]);
            websiteBrowser.GetCurrentWebPage();
            Console.ReadKey();
            websiteBrowser.GetSearchHistory();//2st
            Console.ReadKey();
            websiteBrowser.TryGoForward();
            websiteBrowser.GetCurrentWebPage();
            Console.ReadKey();
            websiteBrowser.GetSearchHistory();//2st
            
            Console.WriteLine("Stopped...");
            Console.ReadKey();
            
        }
        static int GetInput(int startIndex, int maxLenght){
            var result = -1;
            while (true){
                if (int.TryParse(Console.ReadLine(), NumberStyles.Integer, null, out result) &&
                    result == Math.Clamp(result, startIndex, maxLenght)){
                    Console.Clear();
                    return result;
                }

                Console.WriteLine($"Error: Needs to be a number between {startIndex} and {maxLenght}");
            }
        }
        static IWebsiteBrowser GetBrowserOption(){//TODO: Implement and change to using reflection!
            Console.WriteLine("Tiny browser:\nOptions: 1. Live, 2. Offline");
            while (true){
                switch (GetConsoleKey()){
                    case ConsoleKey.D1:
                        return new OnlineWebsiteBrowser();
                    case ConsoleKey.D2:
                        return new OfflineWebsiteBrowser();
                }
            }
        }
        static ConsoleKey GetConsoleKey(){
            var keyInfo = Console.ReadKey();
            return keyInfo.Key;
        }
    }
}