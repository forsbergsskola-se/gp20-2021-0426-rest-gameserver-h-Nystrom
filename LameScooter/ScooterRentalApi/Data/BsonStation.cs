using System;
using MongoDB.Bson.Serialization.Attributes;

namespace LameScooter.ScooterRentalApi.Data{
    public class BsonStation : Station{
        [BsonId] // _id
        public Guid Id{ get; set; }
    }
}