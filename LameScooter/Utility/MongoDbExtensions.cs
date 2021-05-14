using System;
using System.Collections.Generic;
using LameScooter.ScooterRentalApi.Data;
using MongoDB.Driver;

namespace LameScooter.Utility{
    public static class MongoDbExtensions{
        const string ConnectionString = "mongodb://localhost:27017";
        
        public static void InsertMany(this Stations stations){
            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
            try{
                var mongoClient = new MongoClient(mongoClientSettings);
                var mongoDatabase = mongoClient.GetDatabase("lamescooter");
                var collection = mongoDatabase.GetCollection<BsonStation>("station");
                var bsonStations = new List<BsonStation>();
                foreach (var stationElement in stations.stations){
                    bsonStations.Add(new BsonStation{Name = stationElement.Name,
                        BikesAvailable = stationElement.BikesAvailable});
                }
                collection.InsertMany(bsonStations);
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw new Exception(e.GetBaseException().Message);
            }
        }
    }
}