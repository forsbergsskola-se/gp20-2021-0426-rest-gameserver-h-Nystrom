namespace TinyBrowser.Api{
    public interface IWebsiteBrowser{
        int WebPageHtmlCount{ get; }
        string GetCurrentWebPageUri{ get; }
        bool CanReceiveWebPage(string host, string uri, int port);
        string[] GetCurrentWebPage();
        bool TryGoBack();
        bool TryGoForward();
        void TrGoToSubPage(string uri);
        void TryGoToHtmlIndex(int index, int port);
        void GetSearchHistory();
        void DisplaySubPages();
        void DisplayHyperLinks();
        
    }
}