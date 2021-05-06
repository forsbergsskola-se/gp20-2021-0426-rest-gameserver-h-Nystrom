using System;
using System.Linq;
using TinyBrowser.Api.Data;

namespace TinyBrowser.Api.Utility{
    public static class StringToSubPageExtenstion{

        public static ISubPage ConvertToSubPage(this string subPage){
            var test = subPage.Replace("<b>", "").Replace("</b>", "");
            var temp = test.Split("\">");
            
            return new SubPage{
                Uri = temp[0],
                Name = temp[1]
            };
        }

        public static bool IsSubPage(this string subPage){
            var doesntStartWith = new[]{"//","http","mailto"};
            if (doesntStartWith.Any(subPage.StartsWith)){
                return false;
            }
            if(subPage.Contains("src=\""))
                return false;
            return !subPage.Contains(".html", StringComparison.OrdinalIgnoreCase);
        }
    }
}