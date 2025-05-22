using MongoDB.Bson;
using System.Linq.Expressions;

namespace Infrastructuur.Repositories.Interfaces
{
    public interface IMongoRepository<T>
    {
        Task<bool> InsertAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
