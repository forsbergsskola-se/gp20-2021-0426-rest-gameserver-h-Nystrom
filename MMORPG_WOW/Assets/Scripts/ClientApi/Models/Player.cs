using System;
using System.Collections.Generic;

namespace ClientApi.Models{
    [Serializable]
    public class Player : IPlayer, IRequestObject{
        public Guid Id { get; set; }//TODO: Set by server!
        public string Name { get; set; }
        public int Gold{ get; set; }
        public int Xp{ get; set; }
        public List<Item> Items{ get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }//TODO: Set by server!
    }
}