using System;
using MMORPG.ServerApi;

namespace MMORPG.Models{
    public class ModifiedPlayer : IRequestObject{
        public Guid Id{ get; set; }
        public int Score { get; set; }
    }
}