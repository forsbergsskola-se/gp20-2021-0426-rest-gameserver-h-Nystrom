using System;
using System.Linq;
using TinyBrowser.Api.Data;

namespace TinyBrowser.Api.Utility{
    public static class RawHtmlToSubPageExtenstion{

        public static HyperLink ConvertToSubPage(this string subPage){
            var link = subPage.Replace("<b>", "").Replace("</b>", "");
            var nameAndUri = link.Split("\">");
            
            return new HyperLink{
                Uri = nameAndUri[0],
                Description = nameAndUri[1]
            };
        }
        public static bool IsSubPage(this string subPage){
            var doesntStartWith = new[]{"//","http","mailto"};
            return !doesntStartWith.Any(subPage.StartsWith) && !subPage.Contains("src=\"");
        }
    }
}