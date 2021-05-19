using System;

namespace MMORPG.ServerApi{
    public interface IRequestObject{
        Guid Id{ get; set; }
        int Score{ get; set; }
    }
}