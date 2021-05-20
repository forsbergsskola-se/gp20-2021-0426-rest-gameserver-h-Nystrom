using System;

namespace ClientApi.Models{
    public interface IRequestObject{
        Guid Id{ get; set; }
        int Score{ get; set; }
    }
}