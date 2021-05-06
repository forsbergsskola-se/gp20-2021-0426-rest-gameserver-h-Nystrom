namespace LameScooter.Api{
    public interface IStation{
        string Name{ get; }
        int BikesAvailable{ get; }
        string State{ get; }
    }
}