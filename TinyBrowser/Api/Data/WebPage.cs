using System;
using System.Collections.Generic;

namespace TinyBrowser.Api.Data{
    [Serializable]
    public class WebPage : IWebPage{
        public string Title{ get; set; }
        public string Uri{ get; set; }
        public List<ISubPage> SubPages{ get; set; }
    }
}