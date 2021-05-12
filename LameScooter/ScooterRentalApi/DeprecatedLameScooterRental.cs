using System;
using System.IO;
using System.Threading.Tasks;
using LameScooter.ScooterRentalApi.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LameScooter.ScooterRentalApi{
    public class DeprecatedLameScooterRental : ILameScooterRental{

        readonly string databasePath = $"{Environment.CurrentDirectory}/DatabaseDummy/scooters.Json";
        readonly JsonSerializerSettings serializerSettings;
        public DeprecatedLameScooterRental(){
            serializerSettings = new JsonSerializerSettings{
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
        }
        
        public Task<IStation> GetScooterStation(string stationName){
            try{
                var rawJson = File.ReadAllText(databasePath);
                var stations = JsonConvert.DeserializeObject<Station[]>(rawJson, serializerSettings);
                foreach (var station in stations){
                    if (station.Name == stationName)
                        return Task.FromResult((IStation)station);
                }
                throw new NotFoundException($"404: Station {stationName} not found!");
            }
            catch (Exception e){
                throw new Exception(e.GetBaseException().Message);
            }
        }
    }
}