using System;

namespace GitHubExplorer.Data{
    [Serializable]
    public class Repository{
        public string name{ get; set; }
        public Owner owner{ get; set; }
        
    }
}