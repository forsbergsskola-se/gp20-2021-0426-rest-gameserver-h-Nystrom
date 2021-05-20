using System;

namespace ClientApi{
    public interface IRequestObject{
        Guid Id{ get; set; }
        int Gold{ get; set; }
        int Xp{ get; set; }
    }
}