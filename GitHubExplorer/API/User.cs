using System;

namespace GitHubExplorer.API{
    public class User : GitHubRequester, IUser{
        public string Login{ get; set; }
        public string html_url{ get; set; }
        public string organizations_url{ get; set; }
        public string Name{ get; set; }
        public string Company{ get; set; }
        public string Location { get; set; }
        public string Bio{ get; set; }
        public int public_repos{ get; set; }
        public IRepository GetRepository(string repositoryName){
            try{
                return Request<Repository>($"repos/{Login}/{repositoryName}");
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                throw;
            }
        }
        public void ShowInfo(){
            var info = new[]{$"User: {Login}", $"Name: {Name}", 
                $"Bio: {Bio}",$"Location: {Location}", $"Company:{Company}",
                organizations_url, $"Public repositories: {public_repos}", html_url};
            foreach (var element in info){
                Console.WriteLine(element);
            }
        }
        public User() : base(token){ }
    }
}