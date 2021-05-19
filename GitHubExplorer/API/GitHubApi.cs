using System;

namespace GitHubExplorer.API{
    public class GitHubApi : GitHubRequester,IGitHubApi{
        public GitHubApi(string token2) : base(token2){ }
        public IUser GetUser(string userName){
            try{
                return Request<User>($"users/{userName}");
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                throw;
            }
            
        }
        public IOrganization GetOrganization(string organisationName){
            try{
                return Request<Organization>($"orgs/{organisationName}");
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                throw;
            }
        }

        
    }
}