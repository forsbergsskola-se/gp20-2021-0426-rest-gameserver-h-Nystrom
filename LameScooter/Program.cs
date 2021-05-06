using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LameScooter.ScooterRentalApi;

namespace LameScooter{
    internal class Program{
        static async Task Main(string[] args){
            var rental = GetDatabaseType(args);
            
            try{
                var station = await rental.GetScooterStation(args[0].Replace("_", " "));
                Console.WriteLine($"Number of Scooters Available at {station.Name}: {station.BikesAvailable}");
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
            }
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }

        static ILameScooterRental GetDatabaseType(IReadOnlyList<string> args){//TODO: Use reflection here instead!?
            if (args.Count == 2 && args[1] == "live"){
                Console.WriteLine("From server...");
                return new LameScooterRental();
                
            }
            Console.WriteLine("From cached server...");
            return new OfflineLameScooterRental();
        }
    }
}