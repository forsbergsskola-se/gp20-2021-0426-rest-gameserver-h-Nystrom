using System;
using System.Collections.Generic;

namespace LameScooter.ScooterRentalApi{
    public struct LameScooterInitializer{
        public static ILameScooterRental GetLameScooterRental(IReadOnlyList<string> args){
            if(args.Count != 2)
                throw new ArgumentException("Needs two arguments: station name(name without spaces or name_name) and database type (realtime, mongodb, deprecated)");
            return args[1].ToLower() switch{
                "realtime" => new RealTimeLameScooterRental(),
                "mongodb" => throw new NotImplementedException("Implement!"),
                "deprecated" => new DeprecatedLameScooterRental(),
                _ => throw new ArgumentException("Need an argument for database type (realtime, mongodb, deprecated)")
            };
        }
    }
}