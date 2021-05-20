using System;
using System.Collections.Generic;
using ClientApi.Models;

namespace ClientApi{
    public interface IPlayer{
        Guid Id { get;}//TODO: Set by server!
        string Name { get;}
        int Gold{ get; set; }
        int Xp{ get; set; }
        List<Item> Items{ get; set; }
        bool IsDeleted { get; set; }
        DateTime CreationTime { get; set; }
    }
}