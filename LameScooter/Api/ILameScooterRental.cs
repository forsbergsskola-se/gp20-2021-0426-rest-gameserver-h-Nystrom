using System.Threading.Tasks;

namespace LameScooter.Api{
    public interface ILameScooterRental{
        Task<int> GetScooterCountInStation(string stationName);
        
        
    }
}