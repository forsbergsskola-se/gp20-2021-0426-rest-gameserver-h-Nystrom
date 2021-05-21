using System;
using System.Threading.Tasks;
using MMORPG.Models;

namespace MMORPG.ServerApi{
    public interface IRepository<TObject> where TObject: IRequestObject
    {
        Task<TObject> Get(Guid id);
        Task<TObject[]> GetAll();
        Task<TObject> Create(TObject targetObject);
        Task<TObject> Modify(Guid id, string fieldName, int value);
        Task<TObject> UpdateItems<TItem>(Guid id, TItem targetItem) where TItem : Item;
        Task<TObject> Delete(Guid id);
    }
}