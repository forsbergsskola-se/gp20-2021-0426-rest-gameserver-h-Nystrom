using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.ServerApi.Models{
    public class ModifiedPlayer{
        [BsonElement("Score")] public int Score { get; set; }
    }
}