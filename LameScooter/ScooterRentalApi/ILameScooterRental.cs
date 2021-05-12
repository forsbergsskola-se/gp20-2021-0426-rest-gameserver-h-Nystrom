using System.Threading.Tasks;

namespace LameScooter.ScooterRentalApi{
    public interface ILameScooterRental{
        Task<IStation> GetScooterStation(string stationName);
    }
}