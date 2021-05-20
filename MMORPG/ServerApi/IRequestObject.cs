using System;

namespace MMORPG.ServerApi{
    public interface IRequestObject{
        Guid Id{ get; set; }
        int Gold{ get; set; }
        int Xp{ get; set; }
    }
}