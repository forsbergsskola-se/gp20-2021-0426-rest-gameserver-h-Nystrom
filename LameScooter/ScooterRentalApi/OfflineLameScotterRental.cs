using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using LameScooter.ScooterRentalApi.Data;

namespace LameScooter.ScooterRentalApi{
    public class OfflineLameScooterRental : ILameScooterRental{

        readonly string databasePath = $"{Environment.CurrentDirectory}/DatabaseDummy/scooters.Json";
        readonly JsonSerializerOptions options;
        public OfflineLameScooterRental(){
            options = new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        
        public Task<IStation> GetScooterStation(string stationName){
            try{
                var rawJson = File.ReadAllText(databasePath).Replace("\n", "").Trim().Replace(" +", "");
                
                var stations = JsonSerializer.Deserialize<List<Station>>(rawJson,options);
                foreach (var station in stations.Where(station => station.Name == stationName)){
                    return Task.FromResult((IStation)station);
                }
                throw new NotFoundException($"Exception: {stationName} doesn't exist!");
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }
    }
}