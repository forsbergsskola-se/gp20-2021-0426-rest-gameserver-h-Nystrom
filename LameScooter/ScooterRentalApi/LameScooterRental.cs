using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using LameScooter.ScooterRentalApi.Data;

namespace LameScooter.ScooterRentalApi{
    public class LameScooterRental : ILameScooterRental{
        const string ServerUrl = "https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json";
        readonly JsonSerializerOptions options;
        public LameScooterRental(){
            options = new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task<IStation> GetScooterStation(string stationName){
            try{
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(ServerUrl);
                response.EnsureSuccessStatusCode();
                var rawJson = await response.Content.ReadAsStringAsync();
                httpClient.Dispose();
                var stations = JsonSerializer.Deserialize<List<Station>>(rawJson,options);
                foreach (var station in stations.Where(station => station.Name == stationName)){
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