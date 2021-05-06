namespace LameScooter.ScooterRentalApi{
    public interface IStation{
        string Name{ get; }
        int BikesAvailable{ get; }
        string State{ get; }
    }
}