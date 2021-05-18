using System;

namespace MMORPG.ServerApi{
    public interface IObject{
        Guid Id{ get; set; }
        int Score{ get; set; }
    }
}