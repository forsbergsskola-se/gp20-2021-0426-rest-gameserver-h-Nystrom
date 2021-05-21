﻿using System;
using MMORPG.ServerApi;

namespace MMORPG.Models{
    [Serializable]
    public class Item : IRequestObject{
        public Guid Id{ get; set; }
        public string Name{ get; set; }
        public int ItemId{ get; set; }
        public string ItemType{ get; set; }//TODO: Fix to enum or something...
        public DateTime CreationDate{ get; set; }
        public int Gold{ get; set; }
        public int Xp{ get; set; }
    }
}