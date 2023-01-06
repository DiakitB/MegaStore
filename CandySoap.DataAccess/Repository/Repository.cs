using CandySoap.Data;
using CandySoap.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CandySoap.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        internal DbSet<T> dbset;
        public Repository(ApplicationContext context) 
        {
            _context = context;
           // _context.products.Include(u => u.Category).Include(u => u.Covertype);
            this.dbset = _context.Set<T>();

        }
        public void Add(T entity)
        {
            dbset.Add(entity);
        }
        // IncludeProp -"Category, Covertype"
        public IEnumerable<T> GetAll( string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if(includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)) 
                {
                query= query.Include(includeProp);
                }
            }
            return query.ToList();
        }
        // IncludeProp -"Category, Covertype"
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
           IQueryable<T> query = dbset;
            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
           dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbset.RemoveRange(entities);
        }
    }
}
