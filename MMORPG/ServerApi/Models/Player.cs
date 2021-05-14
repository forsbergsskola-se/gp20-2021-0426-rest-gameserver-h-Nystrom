using System;

namespace MMORPG.ServerApi.Models{
    public class Player{
        public Guid Id { get; set; }//TODO: Set by server!
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }//TODO: Set by server!
    }
}