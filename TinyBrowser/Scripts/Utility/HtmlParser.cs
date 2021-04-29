using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TinyBrowser.Scripts.Utility{
    public struct HtmlParser{
        public static string GetTitle(string websiteInfo){
            return Regex.Match(websiteInfo, @"(<title>)(.*?)(</title>)", RegexOptions.IgnoreCase).Groups[2].Value;
        }
        public static List<string> GetSubPages(string websiteInfo){
            return Regex.Matches(websiteInfo, @"(href="")([^//http | // | //mailto][a-zA-Z0-9].*?)("")", RegexOptions.IgnoreCase)
                .Select(m => m.Groups[2].Value)
                .ToList();
        }
    }
}