using Microsoft.EntityFrameworkCore;
using MoscowWeatherAPI.Interfaces;
using System.Linq.Expressions;

namespace MoscowWeatherAPI.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IDataModel
    {
        private bool _disposed;
        protected DbMainContext DbMainContext { get; }
        protected abstract DbSet<T> DbEntities { get; }

        protected BaseRepository(DbMainContext dbMainContext)
        {
            DbMainContext = dbMainContext;
        }

        public abstract T Create(T item);

        public abstract Task<T> CreateAsync(T item);

        public abstract IQueryable<T> Get();

        public abstract IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        public abstract T GetById(Guid id);
        public abstract T Update(T item);

        public void Save()
        {
            DbMainContext.SaveChanges();
        }

        public async virtual Task SaveAsync()
        {
            await DbMainContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DbMainContext.Dispose();
                }
            }
            _disposed = true;
        }
        public abstract bool Delete(Guid id);
        public abstract bool Delete(T item);
        public abstract void CreateRange(IEnumerable<T> items);
        public abstract Task CreateRangeAsync(IEnumerable<T> item);
    }
}
