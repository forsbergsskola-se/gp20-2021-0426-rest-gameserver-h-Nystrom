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
        public string GetCurrentWebPageUri => webPageHistory[currentIndex].Uri;

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
                throw;
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

        public void GetSearchHistory(){//TODO:Fix!
            Console.WriteLine("Search history: ");
            for (var i = 0; i < webPageHistory.Count; i++){
                Console.WriteLine($"({i}){webPageHistory[i].Uri} {webPageHistory[i].Description}");
            }
        }
        public string[] GetCurrentWebPage(){
            return webPageHistory[currentIndex].SubPageDictionary.Keys.ToArray();
        }

        public void DisplayContent(){
            DisplayHyperLinks();
            DisplaySubPages();
        }
        public void DisplaySubPages(){
            var temp = webPageHistory[currentIndex];
            var index = 0;
            var webPages = "Web pages: \n";
            foreach (var (pathName, webPage) in temp.SubPageDictionary){
                webPages += $"({index}) {pathName}/ {webPage.Title}\n";
                index++;
            }
            Console.WriteLine(webPages);
        }
        public void DisplayHyperLinks(){
            var temp = webPageHistory[currentIndex];
            var hyperLinks = $"{temp.Description} {temp.Uri}\nHtml pages:\n";
            for (var i = 0; i < temp.HyperLinks.Count; i++){
                hyperLinks += $"({i}) {temp.HyperLinks[i].Uri} {temp.HyperLinks[i].Description}\n";
            }
            Console.WriteLine(hyperLinks);
        }
    }
}