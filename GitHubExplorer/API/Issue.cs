namespace GitHubExplorer.API{
    public class Issue : IIssue{
        public string UserName{ get; set; }
        public string RepositoryName{ get; set; }
        public string Title{ get; }
        public string Body{ get; }

        public Issue(string title, string body){
            Title = title;
            Body = body;
        }
    }
}