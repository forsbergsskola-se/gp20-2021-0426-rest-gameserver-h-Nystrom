using System;
using LameScooter.ScooterRentalApi.Data;
using MongoDB.Bson.Serialization.Attributes;

namespace LameScooter.Utility{
    public class BsonStation : Station{
        [BsonId] // _id
        public Guid Id{ get; set; }
    }
}