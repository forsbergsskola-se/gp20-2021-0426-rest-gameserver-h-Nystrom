using System.Collections.Generic;

namespace TinyBrowser.Api{
    public interface IWebPage{
        public string Title{ get;}
        public string Uri{ get;}
        public List<ISubPage> SubPages{ get;}
    }
}