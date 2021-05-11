using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TinyBrowser.Api;

namespace TinyBrowser{
    internal class Program{
        //TODO: If I have time: refactor and code clean up.

        const string Host = "acme.com";
        const int Port = 80;
        static IWebsiteBrowser websiteBrowser;
        static void Main(string[] args){

            websiteBrowser = TryGetBrowser();
            if (!websiteBrowser.CanReceiveWebPage(Host, "", Port)){
                Console.WriteLine("Shutting down!");
            }

            var continueFlag = true;
            do{
                Console.Clear();
                Console.WriteLine(websiteBrowser.GetCurrentWebPageUri);
                var uriPages = websiteBrowser.GetCurrentWebPage();
                var optionsMenu = new[]{
                    "Options: ", $"1. Go to link({websiteBrowser.WebPageHtmlCount})",
                    $"2. Go to sub-page({uriPages.Length})", "3. Go forward", "4. Go back",
                    "5. Search history", "6. Exit"
                };
                var input = NavigationOptions(optionsMenu);
                Console.Clear();
                if (input == 6){
                    break;
                }
                Run(input, uriPages);

            } while (continueFlag);
            
            Console.Clear();
            Console.WriteLine("Shutting down!");
        }
        static void Run(int input, IReadOnlyList<string> uriPages){
            switch (input){
                    case 1:
                        SelectHyperLink();
                        break;
                    case 2:
                        SelectSubPage(uriPages);
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
                        websiteBrowser.GetSearchHistory();
                        Console.WriteLine("Press enter to continue!");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
            }
        }

        static void SelectHyperLink(){
            if (websiteBrowser.WebPageHtmlCount == 0){
                Console.WriteLine("No html-links available");
                return;
            }
            websiteBrowser.DisplayHyperLinks();
            Console.WriteLine($"Select hyperlink between 0 and {websiteBrowser.WebPageHtmlCount - 1}");
            var indexInput = GetInput(0, websiteBrowser.WebPageHtmlCount - 1);
            websiteBrowser.TryGoToHtmlIndex(indexInput, Port);
            Console.Clear();
        }

        static void SelectSubPage(IReadOnlyList<string> uriPages){
            if (uriPages.Count == 0){
                Console.WriteLine("No sub-pages available");
                return;
            }
            websiteBrowser.DisplaySubPages();
            Console.WriteLine($"Select sub-page between 0 and {uriPages.Count - 1}");
            var linkOption2 = GetInput(0, uriPages.Count - 1);
            websiteBrowser.TrGoToSubPage(uriPages[linkOption2]);
            Console.Clear();
        }

        static IWebsiteBrowser TryGetBrowser(){
            while (true){
                try{
                    Console.WriteLine("Tiny browser:\nOptions: 1. Live, 2. Offline, 3. XmlReader");
                    var input = GetInput(1, BrowserInitializer.Options);
                    Console.Clear();
                    return BrowserInitializer.GetBrowser(input);
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
                    return result;
                }
                Console.WriteLine($"Number needs to be between: {startIndex} and {maxLenght}");
            }
        }

        static int NavigationOptions(IReadOnlyCollection<string> options){
            var temp = options.Aggregate("", (current, option) => current + $"{option}\n");
            Console.WriteLine(temp);
            var inputValue = GetInput(1, options.Count - 1);
            Console.Clear();
            return inputValue;
        }
    }
}