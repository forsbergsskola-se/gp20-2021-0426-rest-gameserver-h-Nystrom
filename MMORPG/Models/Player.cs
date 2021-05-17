using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Models{
    [Serializable]
    public class Player{
        [BsonId] public Guid Id { get; set; }//TODO: Set by server!
        public string Name { get; set; }
        [BsonElement("Score")] public int Score{ get; set; }
        public int Level{ get; set; }
        public List<Item> Items{ get; set; } = new List<Item>();
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }//TODO: Set by server!
    }
}