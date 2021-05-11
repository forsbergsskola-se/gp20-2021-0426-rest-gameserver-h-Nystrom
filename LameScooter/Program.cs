using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LameScooter.ScooterRentalApi;

namespace LameScooter{
    class Program{
        //TODO: Implement MongoDB!
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

        static ILameScooterRental GetDatabaseType(IReadOnlyList<string> args){
            if(args.Count != 2)
                throw new ArgumentException("Needs two arguments: station name and database type");
            switch (args[1]){
                case "realTime":
                    Console.WriteLine("From deprecated server...");
                    return new LameScooterRental();
                case "MongoDB":
                    throw new NotImplementedException("Implement!");
                case "deprecated":
                    Console.WriteLine("From offline server...");
                    return new OfflineLameScooterRental();
                default:
                    throw new ArgumentException("Database type can't be found!"); 
            }
        }
    }
}