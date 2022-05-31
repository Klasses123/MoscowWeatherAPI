using MoscowWeatherAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MoscowWeatherAPI.Repositories
{
    public class GenericRepository<TEntity> : BaseRepository<TEntity> where TEntity : class, IDataModel
    {
        protected override DbSet<TEntity> DbEntities { get; }

        public GenericRepository(DbMainContext context) : base(context)
        {
            DbEntities = context.Set<TEntity>();
        }

        public override TEntity Create(TEntity item)
        {
            return DbEntities.Add(item).Entity;
        }

        public override async Task<TEntity> CreateAsync(TEntity item)
        {
            return (await DbEntities.AddAsync(item)).Entity;
        }

        public override IQueryable<TEntity> Get()
        {
            return DbEntities;
        }

        public override IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return DbEntities.Where(predicate);
        }

        public override bool Delete(Guid id)
        {
            var entity = DbEntities.Find(id);
            if (entity == null)
                return false;
            DbEntities.Remove(entity);
            return true;
        }

        public override TEntity GetById(Guid id)
        {
            return DbEntities.Find(id);
        }

        public override TEntity Update(TEntity item)
        {
            var res = DbEntities.Update(item).Entity;
            DbMainContext.Entry(item).State = EntityState.Modified;
            return res;
        }

        public override bool Delete(TEntity item)
        {
            var res = DbEntities.Remove(item);

            if (res == null)
                return false;

            return true;
        }

        public override void CreateRange(IEnumerable<TEntity> items)
        {
            DbEntities.AddRange(items);
        }

        public override async Task CreateRangeAsync(IEnumerable<TEntity> item)
        {
            await DbEntities.AddRangeAsync(item);
        }
    }
}
