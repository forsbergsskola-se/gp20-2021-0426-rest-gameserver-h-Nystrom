using System;

namespace GitHubExplorer.API{
    public class GitHubApi : GitHubRequester,IGitHubApi{

        public GitHubApi(string token) : base(token){ }
        
        public IUser GetUser(string userName){
            try{
                var requestUserTask = SendWebRequest(userName);
                requestUserTask.Wait();
                return DeserializeJson<UserData>(requestUserTask.Result);
            }
            catch (AggregateException e){
                throw new AggregateException(e.GetBaseException().Message);
            }
        }
    }
}