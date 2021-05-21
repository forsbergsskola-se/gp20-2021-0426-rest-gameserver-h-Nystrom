namespace ClientApi.Broker.Messages{
    public class UpdateHudMessage{
        public int Gold{ get; }
        public int Xp{ get; }
        public UpdateHudMessage(int gold, int xp){
            Gold = gold;
            Xp = xp;
        }
    }
}