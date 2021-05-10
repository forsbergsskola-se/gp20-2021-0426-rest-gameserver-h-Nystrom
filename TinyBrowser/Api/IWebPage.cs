using System.Collections.Generic;

namespace TinyBrowser.Api{
    public interface IWebPage : IHyperLink{
        public List<IHyperLink> HyperLinks{ get; }
        Dictionary<string, WebPages> SubPageDictionary{ get;}
    }
}