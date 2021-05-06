using System;
using TinyBrowser.Scripts.Data;
using TinyBrowser.Scripts.Utility;

namespace TinyBrowser.Scripts{
    internal class Program{
        const string Host = "acme.com";
        const int Port = 80;

        //TODO: checkout xmlReader
        static void Main(string[] args){
            Setup();
            var webPage = WebRequester.HttpRequest(Host, Port).GetWebPage();
            DisplayCurrentWebPage(webPage);

            Console.ReadKey();
            ClearStorage();
        }

        static void Setup(){
            Storage.SetUpFolders();
        }

        static WebPage GoToWebPage(string host){
            if (!Storage.IsCacheOutDated(host))
                return Storage.GetCachedWebPage(host);

            var webPage = WebRequester.HttpRequest(host, Port).GetWebPage();
            Storage.CacheWebPage(webPage);
            return webPage;
        }

        static void ClearStorage(){
            Console.WriteLine(Storage.Clear());
        }

        static void DisplayCurrentWebPage(WebPage webPage){
            Console.WriteLine(webPage.Host);
            Console.WriteLine(webPage.Title);
            Console.WriteLine(webPage.RawHtml.Length);
            for (var i = 0; i < webPage.SubPages.Count; i++)
                Console.WriteLine($"i: {i}, (Name: {webPage.SubPages[i].Title}), (Link: {webPage.SubPages[i].Uri})");
        }
    }
}