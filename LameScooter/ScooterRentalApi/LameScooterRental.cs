using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LameScooter.ScooterRentalApi.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LameScooter.ScooterRentalApi{
    public class LameScooterRental : ILameScooterRental{
        const string ServerUrl = "https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json";
        readonly JsonSerializerSettings options;
        public LameScooterRental(){
            options = new JsonSerializerSettings{
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
                var stations = JsonConvert.DeserializeObject<Stations>(rawJson, options);
                foreach (var station in stations.stations){
                    if (station.Name == stationName)
                        return station;
                }
                throw new NotFoundException($"Exception: {stationName} doesn't exist!");
            }
            catch (HttpRequestException e){
                throw new HttpRequestException(e.Message);
            }
            catch (Exception e){
                throw new Exception(e.Message);
            }
        }
    }
}