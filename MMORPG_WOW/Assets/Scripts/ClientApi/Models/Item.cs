using System;

namespace ClientApi.Models{
    [Serializable]
    public class Item : IRequestObject{
        public Guid Id{ get; set; }
        public int Score{ get; set; }
    }
}