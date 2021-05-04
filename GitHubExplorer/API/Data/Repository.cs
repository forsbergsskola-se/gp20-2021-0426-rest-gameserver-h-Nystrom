using System;
using System.Collections.Generic;

namespace GitHubExplorer.API.Data{
    [Serializable]
    public class Repository : IRepository{
        public string name{ get; set; }
        public string description{ get; set; }
        public Owner owner{ get; set; }

        public List<IIssue> GetIssues(){
            throw new NotImplementedException();
        }
    }
}