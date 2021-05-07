using System;
using System.Globalization;
using TinyBrowser.Api;

namespace TinyBrowser{
    internal class Program{
        const string Host = "acme.com";
        const int Port = 80;

        static void Main(string[] args){
            var websiteBrowser = GetBrowserOption();

            if (!websiteBrowser.GetWebPage(Host, Port)){
                Console.WriteLine("Shutting down");
                return;
            }
            websiteBrowser.GetCurrentWebPage();
            
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
                        return new WebsiteBrowser();
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