﻿using System;
using System.Collections.Generic;

namespace TinyBrowser.Scripts.Data{
    [Serializable]
    public class WebPage{
        public string RawHtml{ get; }
        public string Host{ get; }
        public string Title{ get; }
        public List<string> SubPages{ get; }
        public WebPage(string rawHtml,string host, string title, List<string> subPages){
            RawHtml = rawHtml;
            Host = host;
            Title = title;
            SubPages = subPages;
        }
    }
}