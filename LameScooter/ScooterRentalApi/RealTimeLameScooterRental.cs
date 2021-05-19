using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LameScooter.ScooterRentalApi.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LameScooter.ScooterRentalApi{
    public class RealTimeLameScooterRental : ILameScooterRental{
        const string ServerUrl = "https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json";
        readonly JsonSerializerSettings serializerSettings;
        public RealTimeLameScooterRental(){
            serializerSettings = new JsonSerializerSettings{
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
        }
        public async Task<IStation> GetScooterStation(string stationName){
            try{
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(ServerUrl);
                response.EnsureSuccessStatusCode();
                var rawJson = await response.Content.ReadAsStringAsync();
                httpClient.Dispose();
                var stations = JsonConvert.DeserializeObject<Stations>(rawJson, serializerSettings);
                foreach (var station in stations.stations.Where(station => station.Name == stationName)){
                    return station;
                }
                throw new NotFoundException($"Station {stationName} was not found!");
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw new Exception(e.GetBaseException().Message);
            }
        }
    }
}