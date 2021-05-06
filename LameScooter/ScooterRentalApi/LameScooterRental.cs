using System.Threading.Tasks;

namespace LameScooter.ScooterRentalApi{
    public class LameScooterRental : ILameScooterRental{
        public Task<IStation> GetScooterStation(string stationName){
            throw new System.NotImplementedException();
        }
    }
}