namespace TinyBrowser.Api{
    public interface IWebsiteBrowser{
        bool GetWebPage(string host, int port);
        void GetCurrentWebPage();
    }
}