namespace LameScooter.Api{
    public interface IStation{
        // int Id{ get; }
        string Name{ get; }
        // int X{ get; }
        // int Y{ get; }
        int BikesAvailable{ get; }
        // int SpacesAvailable{ get; }
        // int Capacity{ get; }
        // bool AllowDropOff{ get; }
        // bool AllowOverloading{ get; }
        // bool IsFloatingBike{ get;}
        // bool IsCarStation{ get; }
        string State{ get; }
        // string[] Networks{ get; }
        // bool RealTimeData{ get; }
    }

    public class Station : IStation{
        public int Id{ get; set; }
        public string Name{ get; set;}
        public int X{ get; set;}
        public int Y{ get; set;}
        public int BikesAvailable{ get; set;}
        public int SpacesAvailable{ get; set;}
        public int Capacity{ get; set;}
        public bool AllowDropOff{ get; set;}
        public bool AllowOverloading{ get; set;}
        public bool IsFloatingBike{ get; set;}
        public bool IsCarStation{ get; set;}
        public string State{ get; set;}
        public string[] Networks{ get; set;}
        public bool RealTimeData{ get; set;}
    }
}