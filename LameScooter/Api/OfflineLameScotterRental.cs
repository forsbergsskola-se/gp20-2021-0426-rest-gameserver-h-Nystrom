using System.Threading.Tasks;

namespace LameScooter.Api{
    public class OfflineLameScooterRental : ILameScooterRental{
        public Task<int> GetScooterCountInStation(string stationName){
            throw new System.NotImplementedException();
        }
    }
}