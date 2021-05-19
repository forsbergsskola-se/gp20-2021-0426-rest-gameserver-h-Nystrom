using System;
using System.Collections.Generic;

namespace ClientApi.Models{
    [Serializable]
    public class Player : IPlayer{
        public Guid Id { get; set; }//TODO: Set by server!
        public string Name { get; set; }
        public int Score{ get; set; }
        public int Level{ get; set; }
        public List<Item> Items{ get; set; } = new List<Item>();
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }//TODO: Set by server!
    }
}