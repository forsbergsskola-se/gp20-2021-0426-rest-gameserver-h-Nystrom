using System;
using TinyBrowser.Scripts.Data;
using TinyBrowser.Scripts.Utility;

namespace TinyBrowser.Scripts{
    class Program{
        const string Host = "acme.com";
        const int Port = 80;
        //TODO: checkout xmlReader
        static void Main(string[] args){
            
            Setup();
            
            try{
                var temp = GoToWebPage(Host);
                DisplayCurrentWebPage(temp);
            }
            catch (Exception e){
                Console.WriteLine(e);
            }
            
            Console.ReadKey();
            ClearStorage();
        }
        static void Setup(){
            Storage.SetUpFolders();
        }
        static WebPage GoToWebPage(string host){
            if (!Storage.IsCacheOutDated(host)) 
                return Storage.GetCachedWebPage(host);
            
            var rawHtml = WebRequester.HttpRequest(host, Port);
            var title = HtmlParser.GetTitle(rawHtml);
            var subPages = HtmlParser.GetSubPages(rawHtml);
            var webPage = new WebPage(rawHtml, host, title, subPages);
            Storage.CacheWebPage(webPage);
            return webPage;
        }
        static void ClearStorage(){
            Console.WriteLine(Storage.Clear());
        }
        static void DisplayCurrentWebPage(WebPage webPage){
            Console.WriteLine(webPage.Title);
            for (var i = 0; i < webPage.SubPages.Count; i++){
                Console.WriteLine($"{i}: {webPage.Host}/{webPage.SubPages[i]}");
            }
        }
    }
}
