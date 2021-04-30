﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TinyBrowser.Scripts.Data;

namespace TinyBrowser.Scripts.Utility{
    public static class WebRequestExtension{
        public static WebPage GetWebPage(this RequestData requestData){
            var title = GeContentBetweenTags(requestData.RawHtml, "<title>", "</title>", 0);
            var subPages = GetSubPages(requestData.RawHtml, requestData.Host);
            return new WebPage(requestData.RawHtml, requestData.Host, title.Item1, subPages);
        }

        static List<SubPage> GetSubPages(string rawHtml, string host){
            var subPages = new List<SubPage>();

            var index = 0;
            while (true){
                var linkResult = GeContentBetweenTags(rawHtml, "<a href=\"", "</a>", index);
                index = linkResult.endIndex;
                try{
                    var temp = GetContentInContent(linkResult.content);
                    
                    if (index >= rawHtml.Length)
                        break;
                    subPages.Add(new SubPage(temp.Uri, temp.Title));
                }
                catch (ArgumentException e){
                    Console.WriteLine(e);
                }
            }

            return subPages;
        }

        static (string content, int endIndex) GeContentBetweenTags(string rawHtml, string startTag, string endTag,
            int currentIndex){
            var startIndex = rawHtml.IndexOf(startTag, currentIndex, StringComparison.OrdinalIgnoreCase) + startTag.Length;
            var endIndex = rawHtml.IndexOf(endTag, startIndex, StringComparison.OrdinalIgnoreCase);
            if (endIndex < currentIndex)
                return ("", rawHtml.Length);
            var content = rawHtml.Substring(startIndex, endIndex - startIndex);
            return (content, endIndex + endTag.Length);
        }

        static SubPage GetContentInContent(string content){
            if (content.StartsWith("//") || content.StartsWith("http", StringComparison.OrdinalIgnoreCase) || 
                content.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Argument exception: Not a subPage ");
            
            var temp = content.Split("\">");
            if (temp.Length < 2)
                return new SubPage(temp[0], "Missing");
            var title = Regex.Replace(temp[^1], @"<[^>]+>", ""); //TODO: Change this.
            
            return temp[0].Length == 1 ? new SubPage("Missing", title) : new SubPage(temp[0], title);
        }
    }
}