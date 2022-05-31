using System.Linq.Expressions;

namespace MoscowWeatherAPI.Interfaces
{    public interface IBaseRepository<T> : IDisposable where T : class
     {
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T GetById(Guid id);
        T Create(T item);
        Task<T> CreateAsync(T item);
        void CreateRange(IEnumerable<T> items);
        Task CreateRangeAsync(IEnumerable<T> item);
        T Update(T item);
        void Save();
        Task SaveAsync();
        bool Delete(Guid id);
        bool Delete(T item);
     }
    
}
