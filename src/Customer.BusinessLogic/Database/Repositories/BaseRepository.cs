using Customer.BusinessLogic.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Customer.DatabaseLogic.Repositories
{

    public class BaseRepository<TEntity> (AppDbContext appDbContext)
        : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly IDbContextFactory<AppDbContext>? _contextFactory;

        //public AppDbContext GetDbContext()
        //{
        //    var contextFactory = (IDbContextFactory<AppDbContext>?)
        //           ServiceLocatorHelper
        //           .ServiceProvider.GetService(typeof(IDbContextFactory<TDbContext>));

        //    if (contextFactory == null)
        //    {
        //        throw new Exception($"{typeof(IDbContextFactory<TDbContext>)} is null");
        //    }

        //    AppDbContext dbContext = contextFactory.CreateDbContext();
        //    return dbContext;
        //}

        private AppDbContext GetDbContext()
        {
            return appDbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var context = GetDbContext();
            await context
                .Set<TEntity>()
                .AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities, int chunkSize = 1000, bool consoleOutput = true)
        {
            // split the entities into chunks of 1000

            //foreach (var entity in entities)
            //{
            //    if (entity.Id == Guid.Empty)
            //    {
            //        entity.Id = Guid.NewGuid();
            //    }
            //}

            var chunks = entities
               .Select((x, i) => new { Index = i, Value = x })
               .GroupBy(x => x.Index / chunkSize)
               .Select(g => g.Select(x => x.Value).ToList())
               .ToList();

            int totalChunks = chunks.Count();
            int counter = 0;

            var context = GetDbContext();
            if (consoleOutput)
            {
                Console.WriteLine();
            }

            //foreach (var chunk in chunks)
            //{
            //    counter++;
            //    context.BulkInsert(chunk);

            //    if (consoleOutput)
            //    {
            //        Console.Write($"\r{counter}/{totalChunks}");
            //    }
            //}

            if (consoleOutput)
            {
                Console.WriteLine();
            }
            return true;
        }

        public async Task<bool> AddRangeAsyncNewNotWorkingWithverylargedata(IEnumerable<TEntity> entities, int chunkSize = 1000)
        {
            // split the entities into chunks of 1000

            var chunks = entities
               .Select((x, i) => new { Index = i, Value = x })
               .GroupBy(x => x.Index / chunkSize)
               .Select(g => g.Select(x => x.Value).ToList())
               .ToList();

            var tasks = new List<Task>();
            foreach (var chunk in chunks)
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var context = GetDbContext())
                    {
                        await context
                        .Set<TEntity>()
                        .AddRangeAsync(chunk);
                        await context.SaveChangesAsync();
                    }

                }));
            }
            await Task.WhenAll(tasks);
            return true;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var context = GetDbContext();
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, int chunkSize = 1000, bool consoleOutput = true)
        {
            // split the entities into chunks of 1000

            //foreach (var entity in entities)
            //{
            //    if (entity.Id == Guid.Empty)
            //    {
            //        entity.Id = Guid.NewGuid();
            //    }
            //}

            var chunks = entities
               .Select((x, i) => new { Index = i, Value = x })
               .GroupBy(x => x.Index / chunkSize)
               .Select(g => g.Select(x => x.Value).ToList())
               .ToList();

            int totalChunks = chunks.Count();
            int counter = 0;

            var context = GetDbContext();
            if (consoleOutput)
            {
                Console.WriteLine();
            }

            //foreach (var chunk in chunks)
            //{
            //    counter++;
            //    await context.BulkInsertOrUpdateAsync(chunk);

            //    if (consoleOutput)
            //    {
            //        Console.Write($"\r{counter}/{totalChunks}");
            //    }
            //}

            if (consoleOutput)
            {
                Console.WriteLine();
            }
            return true;
        }

        public virtual async Task<TEntity> UpdateFieldAsync<TField>(TEntity entity, Expression<Func<TEntity, TField>> fieldSelector)
        {
            var context = GetDbContext();

            // Attach the entity to the context, if not already tracked
            context.Set<TEntity>().Attach(entity);

            // Mark the specified field as modified
            var entry = context.Entry(entity);
            entry.Property(fieldSelector).IsModified = true;

            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            var context = GetDbContext();
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return Guid.Empty;
            }
            return await DeleteAsync(entity);
        }

        public async Task<Guid> DeleteAsync(TEntity entity)
        {
            var idToDelete = entity.Id;
            var context = GetDbContext();
            context.Set<TEntity>().Remove(entity);
            _ = await context.SaveChangesAsync();
            return idToDelete;
        }

        public async Task DeleteAllSlowVersionAsync()
        {
            var context = GetDbContext();
            var allEntities = context.Set<TEntity>().ToList();
            context.Set<TEntity>().RemoveRange(allEntities);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            string tableName = typeof(TEntity).Name.Replace("Entity", "");
            string sql = $"DELETE FROM {tableName}";
            var context = GetDbContext();
            await context.Database.ExecuteSqlRawAsync(sql);
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            if (typeof(TEntity) == typeof(IBaseEntity))
            {
                var context = GetDbContext();

                if (expression == null)
                {
                    return context.Set<TEntity>();
                }
                return context.Set<TEntity>().Where(expression);
            }
            else
            {
                var context = GetDbContext();
                if (expression == null)
                {
                    return context.Set<TEntity>();
                }
                return context.Set<TEntity>().Where(expression);
            }
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var context = GetDbContext();
            var result = await context
                .Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id);
            return result;
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            var context = GetDbContext();
            var result = await context
                .Set<TEntity>()
                .FirstOrDefaultAsync(expression);
            return result;
        }

        public async Task<IQueryable<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> expression)
        {
            var context = GetDbContext();
            var result = await context
                .Set<TEntity>()
                .Where(expression).ToListAsync();
            return result.AsQueryable();
        }
    }
}
