using System;
using System.Threading.Tasks;
using LameScooter.ScooterRentalApi.Data;
using MongoDB.Driver;

namespace LameScooter.ScooterRentalApi{
    public class MongoDbLameScooterRental : ILameScooterRental{
        const string ConnectionString = "mongodb://localhost:27017";
        
        
        public async Task<IStation> GetScooterStation(string stationName){

            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
            try{
                
                var mongoClient = new MongoClient(mongoClientSettings);
                var mongoDatabase = mongoClient.GetDatabase("lamescooter");
                var stationsCollection = mongoDatabase.GetCollection<Station>(stationName);
                throw new NotFoundException($"Station {stationName} was not found!");
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw new Exception(e.GetBaseException().Message);
            }
        }
    }
    // How to find all database names from a host:
    // var temp = mongoClient.ListDatabaseNames().ToList();
    // foreach (var dbName in temp){
    // Console.WriteLine(dbName);
    // }
}