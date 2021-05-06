using System;
using System.Collections.Generic;
using TinyBrowser.Api.Data;

namespace TinyBrowser.Api.Utility{
    public static class StringToWebPageExtenstion{
        static string RawHtml;
        static int currentIndex;
        public static WebPage ConvertHtmlToWebPage(this string rawHtml, string uri){
            RawHtml = rawHtml;
            var title = GetFirstContent("<title>", "</title>");
            var subPages = GetAllContent("<a href=\"", "</a>");
            
            return new WebPage{
                Title = title, 
                Uri = uri, 
                SubPages = subPages
            };
        }
        static string GetFirstContent(string startTag, string endTag){
            var firstIndex = RawHtml.IndexOf(startTag,0, StringComparison.OrdinalIgnoreCase) + startTag.Length;
                if (firstIndex == -1)
                    return "";
                var lastIndex = RawHtml.IndexOf(endTag, firstIndex, StringComparison.OrdinalIgnoreCase);
                if (lastIndex == -1)
                    return "";
                var link = RawHtml.Substring(firstIndex, lastIndex - firstIndex);
                RawHtml = RawHtml.Remove(0, lastIndex);
                return link;
        }
        static List<ISubPage> GetAllContent(string startTag, string endTag){
            var subPages = new List<ISubPage>();
            while (true){
                var link = GetFirstContent(startTag, endTag);
                if(link == "")
                    break;
                if(!link.IsSubPage()) 
                    continue;
                subPages.Add(link.ConvertToSubPage());
                if (currentIndex > 10000){
                    Console.WriteLine(">10k");
                }
            }
            return subPages;
        }
    }
}