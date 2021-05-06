using System;
using System.Threading.Tasks;
using LameScooter.Api;

namespace LameScooter{
    internal class Program{
        static async Task Main(string[] args){
            ILameScooterRental rental = new OfflineLameScooterRental();
            var count = await rental.GetScooterCountInStation(args[0]);
            Console.WriteLine($"Number of Scooters Available at this Station: {count}");
        }
    }
}