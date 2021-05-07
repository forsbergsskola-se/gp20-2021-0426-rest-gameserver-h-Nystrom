using System;
using System.Collections.Generic;
using System.Security;
using TinyBrowser.Api.Data;

namespace TinyBrowser.Api.Utility{
    public static class RawHtmlToWebPageExtenstion{
        static string RawHtml;
        static int currentIndex;

        public static WebPageInitializer ConvertHtmlToWebPage(this string rawHtml, string uri){
            RawHtml = rawHtml;
            rawHtml = "";
            var title = GetFirstContent("<title>", "</title>");
            var links = GetAllContent("<a href=\"", "</a>");
            return WebPageInitializer.Create(links, title);
        }
        static string GetFirstContent(string startTag, string endTag){
            var firstIndex = RawHtml.IndexOf(startTag, 0, StringComparison.OrdinalIgnoreCase) +
                             startTag.Length;
            if (firstIndex == -1)
                return "";
            var lastIndex = RawHtml.IndexOf(endTag, firstIndex, StringComparison.OrdinalIgnoreCase);
            if (lastIndex == -1)
                return "";
            var link = RawHtml.Substring(firstIndex, lastIndex - firstIndex);
            RawHtml = RawHtml.Remove(0, lastIndex);
            return link;
        }

        static List<Link> GetAllContent(string startTag, string endTag){
            var subPages = new List<Link>();
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
    }
}