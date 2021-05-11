namespace GitHubExplorer.API.Data{
    public class PostMessage{
        public string title;
        public string body;

        public PostMessage(string title, string body){
            this.title = title;
            this.body = body;
        }
    }
}