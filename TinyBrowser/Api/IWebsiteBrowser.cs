using System.Collections.Generic;

namespace TinyBrowser.Api{
    public interface IWebsiteBrowser{
        int WebPageHtmlCount{ get; }
        bool CanReceiveWebPage(string host, int port);
        
        //TODO: Move this to an abstract command class!
        string[] GetCurrentWebPage();
        bool TryGoBack();
        bool TryGoForward();
        bool TrGoToSubPage(string uri);
        bool TryGoToHtmlIndex(int index);
        void GetSearchHistory();
    }
}