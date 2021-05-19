using System;
using System.Collections.Generic;

namespace ClientApi.Models{
    [Serializable]
    public class LeaderBoard{
        public List<Player> Player{ get; set; }
    }
}