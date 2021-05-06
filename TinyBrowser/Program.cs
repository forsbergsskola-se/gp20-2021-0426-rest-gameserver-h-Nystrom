using System;
using TinyBrowser.Api;
using TinyBrowser.Api.Data;

namespace TinyBrowser{
    internal class Program{
        const string Host = "acme.com";
        const int Port = 80;

        static void Main(string[] args){
            IWebPage webPage;
            var websiteBrowser = GetBrowserOption();
            try{
                webPage = websiteBrowser.GetWebPage(Host, Port);
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                return;
            }
            Console.Clear();
            
            DisplayCurrentWebPage(webPage);
            
            Console.WriteLine("\nOptions: \n1. Go to subPage, 2. Go back, 3. Exit");
            
            
            Console.ReadKey();
        }

        static void DisplayCurrentWebPage(IWebPage webPage){
            Console.WriteLine(webPage.Title);
            Console.WriteLine($"Total subpages: {webPage.SubPages.Count}");
            for (var i = 0; i < webPage.SubPages.Count; i++){
                Console.WriteLine($"{i}: {webPage.SubPages[i].Uri} {webPage.SubPages[i].Name}");
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