namespace TinyBrowser.Api{
    public interface IWebsiteBrowser{
        int WebPageHtmlCount{ get; }
        bool CanReceiveWebPage(string host, string uri, int port);
        string[] GetCurrentWebPage();
        bool TryGoBack();
        bool TryGoForward();
        bool TrGoToSubPage(string uri);
        bool TryGoToHtmlIndex(int index, int port);
        void GetSearchHistory();
    }
}