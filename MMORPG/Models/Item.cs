using System;

namespace MMORPG.Models{
    [Serializable]
    public class Item{
        public string Name{ get; set; }
        public int Level{ get; set; }
        public string ItemType{ get; set; }//TODO: Fix to enum or something...
        public DateTime CreationDate{ get; set; }
    }
}