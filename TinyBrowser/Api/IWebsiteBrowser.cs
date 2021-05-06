namespace TinyBrowser.Api{
    public interface IWebsiteBrowser{
        IWebPage GetWebPage(string host, int port);
    }
}