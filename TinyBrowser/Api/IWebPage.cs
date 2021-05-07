using System.Collections.Generic;
using TinyBrowser.Api.Data;

namespace TinyBrowser.Api{
    public interface IWebPage{
        public string Title{ get;}
        public string Uri{ get;}
    }
}