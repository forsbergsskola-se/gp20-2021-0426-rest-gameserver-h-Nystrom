using System;
using System.Linq;
using TinyBrowser.Api.Data;

namespace TinyBrowser.Api.Utility{
    public static class RawHtmlToSubPageExtenstion{

        public static Link ConvertToSubPage(this string subPage){
            var test = subPage.Replace("<b>", "").Replace("</b>", "");
            var temp = test.Split("\">");
            
            return new Link{
                Uri = temp[0],
                Name = temp[1]
            };
        }
        public static bool IsSubPage(this string subPage){
            var doesntStartWith = new[]{"//","http","mailto"};
            return !doesntStartWith.Any(subPage.StartsWith) && !subPage.Contains("src=\"");
        }
    }
}