using System.Collections.Generic;
using GitHubExplorer.API.Data;

namespace GitHubExplorer.API{
    public interface IUser {
        
        /// <summary>
        ///
        /// UserProfile:
        /// Login
        /// Name
        /// Location
        /// Bio
        /// email
        /// blog
        /// twitterUserName
        /// Company
        /// hireable
        ///
        /// 
        /// html_url
        ///
        ///
        /// Repos_url
        /// public repos (int)
        ///
        /// Follow
        /// Following
        /// </summary>
        string login{ get; }
        string name {get;}
        string bio{ get; }
        string location {get;}
        string company{ get; }
        string html_url{ get; }
        int public_repos{ get; }
        List<IRepository> Repositories{ get; }
        void DisplayInfo();

    }
}