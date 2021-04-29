using System.Collections.Generic;

namespace TinyBrowser.Scripts.Data{
    public class WebPage{
        public string RawHtml{ get; }
        public string Title{ get; }
        public List<string> SubPages{ get; }
        public WebPage(string rawHtml, string title, List<string> subPages){
            RawHtml = rawHtml;
            Title = title;
            SubPages = subPages;
        }
    }
}