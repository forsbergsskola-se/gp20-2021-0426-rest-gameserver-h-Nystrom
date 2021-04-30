namespace TinyBrowser.Scripts.Data{
    public class RequestData{
        public string RawHtml;
        public string Host;
        public RequestData(string rawHtml, string host){
            RawHtml = rawHtml;
            Host = host;
        }
    }
}