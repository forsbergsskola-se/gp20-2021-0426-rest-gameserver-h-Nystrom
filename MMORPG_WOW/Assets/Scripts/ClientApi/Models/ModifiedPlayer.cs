using System;

namespace ClientApi.Models{
    public class ModifiedPlayer{
        public Guid Id{ get; set; }
        public int Gold{ get; set; }
        public int Xp { get; set; }
    }
}