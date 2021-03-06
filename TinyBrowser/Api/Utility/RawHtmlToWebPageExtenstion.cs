using System;
using System.Collections.Generic;

namespace TinyBrowser.Api.Utility{
    public static class RawHtmlToWebPageExtenstion{
        static string _rawHtml;

        public static IWebPage ConvertHtmlToWebPage(this string rawHtml){
            _rawHtml = rawHtml;
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
            
            var link = _rawHtml.Substring(firstIndex, lastIndex - firstIndex);
            _rawHtml = _rawHtml.Remove(0, lastIndex);
            return link;
        }
        static bool TryGetIndexOf(string tag, int startIndex, out int resultIndex){
            resultIndex = _rawHtml.IndexOf(tag, startIndex, StringComparison.OrdinalIgnoreCase);
            return resultIndex <= -1;
        }
        static List<IHyperLink> GetAllContent(string startTag, string endTag){
            var subPages = new List<IHyperLink>();
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
            public string Uri{ get; }
            public string Description{ get; }
            public List<IHyperLink> HyperLinks{ get; }
            public Dictionary<string, WebPages> SubPageDictionary{ get; }

            public WebPageData(string description, List<IHyperLink> links){
                Description = description;
                HyperLinks = links;
            }
        }
    }
}