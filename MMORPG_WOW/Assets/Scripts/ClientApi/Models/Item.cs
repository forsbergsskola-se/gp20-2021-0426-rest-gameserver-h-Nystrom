using System;

namespace ClientApi.Models{
    [Serializable]
    public class Item : IRequestObject{
        public Guid Id{ get; set; }
        public string Name{ get; set; }
        public int Gold{ get; set; }
        public int Xp{ get; set; }
        public DateTime CreationDate{ get; set; }
        public string ItemType{ get; set; }
    }
}