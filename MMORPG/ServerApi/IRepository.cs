using System;
using System.Threading.Tasks;

namespace MMORPG.ServerApi{
    public interface IRepository<TObject> where TObject: IRequestObject
    {
        Task<TObject> Get(Guid id);
        Task<TObject[]> GetAll();
        Task<TObject> Create(TObject targetObject);
        Task<TObject> Modify<TObject2>(Guid id, TObject2 targetObject)where TObject2 : IRequestObject;
        Task<TObject> Delete(Guid id);
    }
}