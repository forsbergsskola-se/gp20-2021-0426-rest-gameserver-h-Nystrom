using System;
using MMORPG.ServerApi;

namespace MMORPG.Models{
    public class ModifiedPlayer : IRequestObject{
        public Guid Id{ get; set; }
        public int Gold { get; set; }
        public int Xp{ get; set; }
    }
}