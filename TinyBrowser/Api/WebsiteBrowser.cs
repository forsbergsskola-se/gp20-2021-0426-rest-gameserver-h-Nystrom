using System;
using System.Collections.Generic;
using System.Linq;
using TinyBrowser.Api.Utility;

namespace TinyBrowser.Api{
    public abstract class WebsiteBrowser : IWebsiteBrowser{
        IWebPage homeWebPage;
        List<IWebPage> webPageHistory = new List<IWebPage>();
        int currentIndex;
        
        public int WebPageHtmlCount => webPageHistory[currentIndex].HyperLinks.Count;

        public bool CanReceiveWebPage(string host, string uri, int port){
            try{
                var rawHtml = GetWebPageHtml(host, uri, port);
                homeWebPage = rawHtml.ConvertHtmlToWebPage();
                homeWebPage = WebPages.SortPages(homeWebPage, host);
                webPageHistory.Add(homeWebPage);
                currentIndex = webPageHistory.Count-1;
                return true;
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                return false;
            }
        }

        protected virtual string GetWebPageHtml(string host,string uri, int port){
            throw new NotImplementedException("Method not implemented!");
        }
        public bool TryGoBack(){
            if(currentIndex <= 0)
                return false;
            currentIndex--;
            return true;
        }
        public bool TryGoForward(){
            if(currentIndex >= webPageHistory.Count-1)
                return false;
            currentIndex++;
            return true;
        }
        public bool TrGoToSubPage(string uri){
            if (!webPageHistory[currentIndex].SubPageDictionary.ContainsKey(uri)) return false;
            if(currentIndex < webPageHistory.Count-1)
                webPageHistory.RemoveRange(currentIndex+1,webPageHistory.Count-currentIndex-1);
            
            webPageHistory.Add(webPageHistory[currentIndex].SubPageDictionary[uri]);
            currentIndex++;
            return true;
        }
        public bool TryGoToHtmlIndex(int index, int port){
            var uri = webPageHistory[currentIndex].HyperLinks[index].Uri;
            var host = homeWebPage.Uri;
            return CanReceiveWebPage(host, uri, port);
        }

        public void GetSearchHistory(){
            Console.WriteLine("Search history: ");
            for (var i = 0; i < webPageHistory.Count; i++){
                Console.WriteLine($"({i}) {webPageHistory[i].Uri} {webPageHistory[i].Description}");
            }
        }
        public string[] GetCurrentWebPage(){
            var temp = webPageHistory[currentIndex];
            DisplayHyperLinks(temp);
            DisplaySubPages(temp);
            return temp.SubPageDictionary.Keys.ToArray();
        }
        static void DisplaySubPages(IWebPage temp){
            Console.WriteLine("\nWeb pages:");
            var index = 0;
            foreach (var (pathName, webPage) in temp.SubPageDictionary){
                Console.WriteLine($"({index}) {pathName}/ {webPage.Title} ");
                index++;
            }
        }
        static void DisplayHyperLinks(IWebPage temp){
            Console.WriteLine($"{temp.Description} {temp.Uri}");
            Console.WriteLine("Html pages:");
            for (var i = 0; i < temp.HyperLinks.Count; i++){
                Console.WriteLine($"({i}) {temp.HyperLinks[i].Uri} {temp.HyperLinks[i].Description}");
            }
        }
    }
}