using System;
using System.Collections.Generic;
using MMORPG.ServerApi;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Models{
    [Serializable]
    public class Player : IRequestObject, IComparable<Player>{
        [BsonId] public Guid Id { get; set; }//TODO: Set by server!
        public string Name { get; set; }
        public int Gold{ get; set; }
        public int Xp{ get; set; }
        public List<Item> Items{ get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }//TODO: Set by server!
        public int CompareTo(Player other){
            return Xp.CompareTo(other.Xp);
        }
    }
}