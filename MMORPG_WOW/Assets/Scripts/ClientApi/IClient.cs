using System;
using System.Threading.Tasks;

namespace ClientApi{
    public interface IClient{
        Task<string> PostWebRequest<TObject>(string uri, TObject targetObject);
        Task<string> GetWebRequest(string uri);
        Task<string> DeleteTargetObject(string uri);
    }
}