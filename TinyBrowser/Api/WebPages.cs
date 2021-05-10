using System.Collections.Generic;
using System.Linq;
using TinyBrowser.Api.Data;

namespace TinyBrowser.Api{
    public class WebPages : IWebPage{
        public string Title{ get; }
        public string Uri{ get; }
        public string Description{ get; }
        public List<IHyperLink> HyperLinks{ get; set; }
        public Dictionary<string, WebPages> SubPageDictionary{ get; set;}

        WebPages(string title, string uri){
            Title = title;
            Uri = uri;
            SubPageDictionary = new Dictionary<string, WebPages>();
            HyperLinks = new List<IHyperLink>();
        }

        public static IWebPage SortPages(IWebPage webPage, string uri){
            var sortedWebPage = new WebPages(webPage.Description, uri);
            foreach (var link in webPage.HyperLinks){
                
                var splitLink = link.Uri.Trim().Split("/");
                if (ContainsOnlyOneIndex(splitLink)){
                    if (IsHtmlLink(splitLink[0])){
                        sortedWebPage.HyperLinks.Add(link);
                    }
                    else{
                        sortedWebPage.SubPageDictionary.Add(splitLink[0], 
                            new WebPages(link.Description, splitLink[0]));
                    }
                }
                else{
                    sortedWebPage.TryAdd(splitLink, link);
                }
            }
            return sortedWebPage;
        }

        void TryAdd(IReadOnlyList<string> uri, IHyperLink hyperLink){
            if (uri[0] == "")
                return;
            if (IsHtmlLink(uri[0])){
                HyperLinks.Add(hyperLink);
                return;
            }
            if (!SubPageDictionary.ContainsKey(uri[0])){
                var webPageInstance = new WebPages(hyperLink.Description, uri[0]);
                SubPageDictionary.Add(uri[0], webPageInstance);
                if (uri.Count > 0)
                    webPageInstance.TryAdd(uri.Skip(1).ToArray(), hyperLink);
                return;
            }
            if (uri.Count > 0){
                SubPageDictionary[uri[0]].TryAdd(uri.Skip(1).ToArray(), hyperLink);
            }
        }

        static bool ContainsOnlyOneIndex(IReadOnlyList<string> splitUri){
            return splitUri.Count == 1 || splitUri[1] == "";
        }
        static bool IsHtmlLink(string link){
            return link.EndsWith(".html");
        }
    }
}