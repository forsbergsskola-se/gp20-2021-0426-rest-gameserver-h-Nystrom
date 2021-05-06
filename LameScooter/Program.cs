using System;
using System.Threading.Tasks;
using LameScooter.ScooterRentalApi;

namespace LameScooter{
    internal class Program{
        static async Task Main(string[] args){
            ILameScooterRental rental = new OfflineLameScooterRental();
            Console.WriteLine(string.Join(' ', args));
            try{
                var station = await rental.GetScooterStation(args[0].Replace("_", " "));
                Console.WriteLine($"Number of Scooters Available at {station.Name}: {station.BikesAvailable}");
                if (args.Length == 2 && args[1].ToLower() == "current_state"){
                 Console.WriteLine($"Current state: {station.State}");   
                }
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
            }
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }
    }
}