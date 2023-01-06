using System.Linq.Expressions;

namespace CandySoap.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);  
        void RemoveRange(IEnumerable<T> entities);
    }
}
