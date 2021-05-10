using System;
using System.Collections.Generic;

namespace TinyBrowser.Api.Utility{
    public static class RawHtmlToWebPageExtenstion{
        static string RawHtml;
        static int currentIndex;

        public static IWebPage ConvertHtmlToWebPage(this string rawHtml){
            RawHtml = rawHtml;
            var title = GetFirstContent("<title>", "</title>");
            var links = GetAllContent("<a href=\"", "</a>");
            return new WebPageData(title, links);
        }
        static string GetFirstContent(string startTag, string endTag){
            if (TryGetIndexOf(startTag, 0, out var firstIndex))
                return "";
            firstIndex += startTag.Length;
            
            if (TryGetIndexOf(endTag, firstIndex, out var lastIndex))
                return "";
            
            var link = RawHtml.Substring(firstIndex, lastIndex - firstIndex);
            RawHtml = RawHtml.Remove(0, lastIndex);
            return link;
        }
        static bool TryGetIndexOf(string tag, int startIndex, out int resultIndex){
            resultIndex = RawHtml.IndexOf(tag, startIndex, StringComparison.OrdinalIgnoreCase);
            return resultIndex <= -1;
        }
        static List<ILink> GetAllContent(string startTag, string endTag){
            var subPages = new List<ILink>();
            while (true){
                var link = GetFirstContent(startTag, endTag);
                if(link == "")
                    break;
                if(!link.IsSubPage()) 
                    continue;
                subPages.Add(link.ConvertToSubPage());
            }
            return subPages;
        }
        class WebPageData : IWebPage{
            public string Title{ get; }
            public string Uri{ get; }
            public List<ILink> HyperLinks{ get; }
            public Dictionary<string, WebPages> SubPageDictionary{ get; }

            public WebPageData(string title, List<ILink> links){
                Title = title;
                HyperLinks = links;
            }
        }
    }
}