using System;
using System.Collections.Generic;
using System.Linq;
using TinyBrowser.Api.Utility;

namespace TinyBrowser.Api{
    public abstract class WebsiteBrowser{
        protected IWebPage HomeWebPage;
        protected List<IWebPage> WebPageHistory;
        int currentIndex;
        
        public int WebPageHtmlCount => HomeWebPage.HyperLinks.Count;

        public bool CanReceiveWebPage(string host, int port){
            try{
                var rawHtml = GetWebPageHtml(host, port);
                HomeWebPage = rawHtml.ConvertHtmlToWebPage();
                HomeWebPage = WebPages.SortPages(HomeWebPage, host);
                WebPageHistory = new List<IWebPage>{HomeWebPage};
                return true;
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                return false;
            }
        }

        protected virtual string GetWebPageHtml(string host, int port){
            return "";
        }
        
        public string[] GetCurrentWebPage(){
            Console.Clear();
            var temp = WebPageHistory[currentIndex];
            Console.WriteLine($"{temp.Title} {temp.Uri}");
            Console.WriteLine("Html pages:");
            for (var i = 0; i < temp.HyperLinks.Count; i++){
                Console.WriteLine($"({i}) {temp.HyperLinks[i].Uri} {temp.HyperLinks[i].Name}");
            }
            Console.WriteLine("\nWeb pages:");
            var index = 0;
            foreach (var subPage in temp.SubPageDictionary){
                Console.WriteLine($"({index}) {subPage.Key}/ {subPage.Value.Title} ");
                index++;
            }
            return temp.SubPageDictionary.Keys.ToArray();
        }
        public bool TryGoBack(){
            if(currentIndex <= 0)
                return false;
            currentIndex--;
            return true;
        }
        public bool TryGoForward(){
            if(currentIndex >= WebPageHistory.Count-1)
                return false;
            currentIndex++;
            return true;
        }
        public bool TrGoToSubPage(string uri){
            if (!WebPageHistory[currentIndex].SubPageDictionary.ContainsKey(uri)) return false;
            if(currentIndex < WebPageHistory.Count-1)
                WebPageHistory.RemoveRange(currentIndex+1,WebPageHistory.Count-currentIndex-1);
            
            WebPageHistory.Add(WebPageHistory[currentIndex].SubPageDictionary[uri]);
            currentIndex++;
            return true;
        }

        public bool TryGoToHtmlIndex(int index){
            throw new NotImplementedException();
        }

        public void GetSearchHistory(){
            Console.WriteLine("Search history: ");
            for (int i = 0; i < WebPageHistory.Count; i++){
                Console.WriteLine($"({i}) {WebPageHistory[i].Uri} {WebPageHistory[i].Title}");
            }
        }
    }
}