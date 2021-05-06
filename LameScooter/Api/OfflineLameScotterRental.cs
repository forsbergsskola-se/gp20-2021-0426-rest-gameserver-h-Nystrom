using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LameScooter.Api{
    public class OfflineLameScooterRental : ILameScooterRental{

        readonly string databasePath = $"{Environment.CurrentDirectory}/DatabaseDummy/scooters.Json";
        
        public Task<int> GetScooterCountInStation(string stationName){
            try{
                
                var rawJson = File.ReadAllText(databasePath);
                var stations = JsonConvert.DeserializeObject<Station[]>(rawJson);
                foreach (var station in stations){
                    if (station.Name == stationName){
                        return Task.FromResult(station.BikesAvailable);
                    }
                }
                throw new ArgumentException($"Exception: {stationName} doesn't exist!");
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }
    }
}