using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using LameScooter.Api.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LameScooter.Api{
    public class OfflineLameScooterRental : ILameScooterRental{

        readonly string databasePath = $"{Environment.CurrentDirectory}/DatabaseDummy/scooters.Json";
        readonly JsonSerializerSettings options;
        public OfflineLameScooterRental(){
            options = new JsonSerializerSettings{
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
        }
        
        public Task<int> GetScooterCountInStation(string stationName){
            try{
                var rawJson = File.ReadAllText(databasePath);
                var stations = JsonConvert.DeserializeObject<List<Station>>(rawJson, options);
                foreach (var station in stations.Where(station => station.Name == stationName)){
                    return Task.FromResult(station.BikesAvailable);
                }
                throw new ArgumentException($"Exception: {stationName} doesn't exist!");
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }
    }
    //using System.Text.Json;
    //In Constructor:
    // options = new JsonSerializerOptions{
    // PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    // };
    //In method:
    // Formatting = Formatting.Indented
    // var rawJson = File.ReadAllText(databasePath).Replace("\n","").Replace(" ", "");
    // var stations = JsonSerializer.Deserialize<List<Station>>(rawJson,options);
}