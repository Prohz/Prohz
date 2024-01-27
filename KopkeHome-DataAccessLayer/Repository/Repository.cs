using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KopkeHome_DataAccessLayer.Repository
{
#nullable disable
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext context;
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> GetSingle(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetMultipleRecord()
        {
            return await context.Set<TEntity>().ToListAsync();
        }
        public async Task<List<TEntity>> FindAllByCondition(Expression<Func<TEntity, bool>> match)
        {
            return await context.Set<TEntity>().Where(match).ToListAsync();
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Detached;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> FindSingleByCondition(Expression<Func<TEntity, bool>> match)
        {
            return await context.Set<TEntity>().Where(match).FirstOrDefaultAsync();
        }
    }

}
