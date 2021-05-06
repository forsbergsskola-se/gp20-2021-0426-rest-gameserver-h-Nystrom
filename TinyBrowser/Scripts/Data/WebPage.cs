using System;
using System.Collections.Generic;

namespace TinyBrowser.Scripts.Data{
    [Serializable]
    public class WebPage{
        public WebPage(string rawHtml, string host, string title, List<SubPage> subPages){
            RawHtml = rawHtml;
            Host = host;
            Title = title;
            SubPages = subPages;
        }

        public string RawHtml{ get; }
        public string Host{ get; }
        public string Title{ get; }
        public List<SubPage> SubPages{ get; }
    }
}