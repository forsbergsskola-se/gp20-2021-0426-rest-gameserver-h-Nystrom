using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LameScooter.ScooterRentalApi;

namespace LameScooter{
    class Program{
        //TODO: Implement MongoDB!
        
        static async Task Main(string[] args){
            try{
                NoArgsCheck(ref args);
                DigitCheck(args[0]);
                var rental = LameScooterInitializer.GetLameScooterRental(args);
                var station = await rental.GetScooterStation(args[0].Replace("_", " "));
                Console.WriteLine($"Number of Scooters Available at {station.Name}: {station.BikesAvailable}");
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
            }
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }

        static void DigitCheck(string arg){
            if (Regex.Match(arg, @"[0-9]", RegexOptions.IgnoreCase).Success){
                throw new ArgumentException("Stations can't contain digits!");
            }
        }

        static void NoArgsCheck(ref string[] args){
            
            if (args.Length != 0) return;
            var options = new[]{"deprecated","realtime","mongodb"};
            Console.WriteLine($"No args! Press: 1. {options[1]}, 2. {options[2]}, Other: {options[0]}");
            switch (Console.ReadKey().Key){
                case ConsoleKey.D1:
                    args = new []{"Linnanmäki", options[1]};
                    return;
                case ConsoleKey.D2:
                    args = new []{"Linnanmäki", options[2]};
                    return;
                default:
                    args = new []{"Linnanmäki", options[0]}; 
                    return;
            }
            //TODO: Replace with:
            //throw new ArgumentException("Server needs a station name!");

        }
    }
}