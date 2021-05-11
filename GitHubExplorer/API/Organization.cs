using System;
using System.Collections.Generic;
using System.Linq;

namespace GitHubExplorer.API{
    public class Organization : GitHubRequester, IOrganization{
        public string Login{ get; set; }
        public Organization() : base(token){ }
        public List<IUser> GetMembers(){
            try{
                var members = Request<List<User>>($"orgs/{Login}/members");
                return members.Cast<IUser>().ToList();
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                throw;
            }
        }

        
    }
}