using System;
using MMORPG.ServerApi;

namespace MMORPG.Models{
    public class NewPlayer : IRequestObject{
        public string Name { get; set; }
        public Guid Id{ get; set; }
        public int Gold{ get; set; }
        public int Xp{ get; set; }
    }
}