using System;
using System.Threading.Tasks;

namespace ClientApi{
    public interface IClient{
        Task<string> GetTargetObject(Guid id);
        Task<string> CreateTargetObject<TObject>(string uri, TObject targetObject);
        Task<string> GetTargetObjects(string uri);
        Task<string> ModifyTargetObject<TObject>(string uri, TObject targetObject);
        Task<string> DeleteTargetObject<TObject>(string uri, TObject targetObject);
    }
}