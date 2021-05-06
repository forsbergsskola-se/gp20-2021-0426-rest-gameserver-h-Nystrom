namespace TinyBrowser.Scripts.Data{
    public class RequestData{
        public string Host;
        public string RawHtml;

        public RequestData(string rawHtml, string host){
            RawHtml = rawHtml;
            Host = host;
        }
    }
}