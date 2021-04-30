using System;

namespace TinyBrowser.Scripts.Data{
    [Serializable]
    public class SubPage{
        public string Uri;
        public string Title;

        public SubPage(string uri, string title){
            Uri = uri;
            Title = title;
        }
    }
}