using System;
using System.Collections.Generic;
using System.Globalization;
using TinyBrowser.Api;

namespace TinyBrowser{
    internal class Program{
        //TODO: 1. Clean up code!
        //TODO: 2. Fix search history!
        //TODO: 3. Split long descriptions!
        
        
        const string Host = "acme.com";
        const int Port = 80;
        static IWebsiteBrowser websiteBrowser;
        static void Main(string[] args){

            websiteBrowser = TryGetBrowser();
            if (!websiteBrowser.CanReceiveWebPage(Host, "",Port)){
                Console.WriteLine("Program is shutting down!");
                return;
            }
            while (true){
                
                var uriPages = websiteBrowser.GetCurrentWebPage();
                var optionsMenu = new[] {
                    "Options: ", $"1. Go to link({websiteBrowser.WebPageHtmlCount})", $"2. Go to sub-page({uriPages.Length})", "3. Go forward", "4. Go back",
                    "5. Search history"};
                var input = NavigationOptions(optionsMenu);
                Run(input, uriPages);
                Console.WriteLine("Press Space to exit, press any other key to continue!");
                if(GetConsoleKey() == ConsoleKey.Spacebar)
                    return;
                Console.Clear();
            }
        }

        static void Run(int input, IReadOnlyList<string> uriPages){
            switch (input){
                    case 1:
                        if (websiteBrowser.WebPageHtmlCount == 0){
                            Console.WriteLine("No html-links available");
                            break;
                        }
                        Console.WriteLine($"Select htlm-link between 0 and {websiteBrowser.WebPageHtmlCount-1}");
                        var linkOption = GetInput(0, websiteBrowser.WebPageHtmlCount-1);
                        websiteBrowser.TryGoToHtmlIndex(linkOption, Port);
                        break;
                    case 2:
                        if (uriPages.Count == 0){
                            Console.WriteLine("No sub-pages available");
                            break;
                        }
                        Console.WriteLine($"Select sub-page between 0 and {uriPages.Count - 1}");
                        var linkOption2 = GetInput(0, uriPages.Count - 1);
                        websiteBrowser.TrGoToSubPage(uriPages[linkOption2]);
                        break;
                    case 3:
                        if (!websiteBrowser.TryGoForward()){
                            Console.WriteLine("Can't go forward!");
                        }
                        break;
                    case 4:
                        if (!websiteBrowser.TryGoBack()){
                            Console.WriteLine("Can't go back!");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Show search history");
                        websiteBrowser.GetSearchHistory();
                        break;
            }
        }

        static IWebsiteBrowser TryGetBrowser(){
            while (true){
                Console.WriteLine("Tiny browser:Options: 1. Live, 2. Offline");
                try{
                    return BrowserInitializer.GetBrowser(GetConsoleKey());
                }
                catch (Exception e){
                    Console.WriteLine(e.GetBaseException().Message);
                }
            }
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
        static int NavigationOptions(IReadOnlyCollection<string> options){
            foreach (var option in options){
                Console.WriteLine(option);
            }
            var inputValue = GetInput(1, options.Count);
            Console.Clear();
            return inputValue;
        }
        static ConsoleKey GetConsoleKey(){
            var keyInfo = Console.ReadKey();
            Console.Clear();
            return keyInfo.Key;
        }
    }
}