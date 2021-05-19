using System;
using System.Collections.Generic;
using ClientApi.Models;

namespace ClientApi{
    public interface IPlayer{
        Guid Id { get;}//TODO: Set by server!
        string Name { get;}
        public int Score{ get; set; }
        public int Level{ get; set; }
        // public List<Item> Items{ get; set; } = new List<Item>();
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}