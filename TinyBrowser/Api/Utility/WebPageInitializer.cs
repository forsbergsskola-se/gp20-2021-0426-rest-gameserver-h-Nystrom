using System.Collections.Generic;
using System.Linq;
using TinyBrowser.Api.Data;

namespace TinyBrowser.Api.Utility{
    public class WebPageInitializer : IWebPage{
        public string Title{ get; }
        public string Uri{ get; } 
        public Link MainLink{ get; }//TODO: Fix!
        Dictionary<string, WebPageInitializer> subPages = new Dictionary<string, WebPageInitializer>();
        List<Link> HtmlList = new List<Link>();

        WebPageInitializer(string title, Link mainLink){
            Title = title;
            MainLink = mainLink;
        }

        public static WebPageInitializer Create(IEnumerable<Link> links, string title){//TODO:Refactor this!
            var webPageDictionary = new WebPageInitializer(title, new Link{Name = title, Uri = "acme.com"});
            foreach (var link in links){
                var element = link.Uri.Trim().Split("/");
                if (element.Length == 1 || element[1] == ""){
                    if (!element[0].EndsWith(".html")){
                        webPageDictionary.AddNewSubPage(element[0], link);
                    }
                    else{
                        webPageDictionary.HtmlList.Add(link);
                    }
                }
                else{
                    webPageDictionary.Add(element, link);
                }
            }
            return webPageDictionary;
        }

        protected void Add(string[] uri, Link link){
            if (uri[0] == "")
                return;
            if (!uri[0].EndsWith(".html")){
                if (!subPages.ContainsKey(uri[0])){
                    AddToNewSubPage(uri, link);
                }
                else{
                    AddToOwnSubPages(uri, link);
                }
            }
            else{
                HtmlList.Add(link);
            }
        }

        protected void AddNewSubPage(string key, Link value){
            subPages.Add(key, new WebPageInitializer(key, value));
        }

        void AddToNewSubPage(string[] uri, Link link){
            var temp = new WebPageInitializer(uri[0], link);
            subPages.Add(uri[0], temp);
            if (uri.Length > 0)
                temp.Add(uri.Skip(1).ToArray(), link);
        }

        void AddToOwnSubPages(string[] uri, Link link){
            if (uri.Length > 0)
                subPages[uri[0]].Add(uri.Skip(1).ToArray(), link);
        }
    }
}