using System;

namespace TinyBrowser.Scripts.Data{
    [Serializable]
    public class SubPage{
        public string Title;
        public string Uri;

        public SubPage(string uri, string title){
            Uri = uri;
            Title = title;
        }
    }
}