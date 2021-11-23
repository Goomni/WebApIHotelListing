using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApIHotelListing.Data;
using WebApIHotelListing.IRepository;

namespace WebApIHotelListing.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDataContext _context;
        private readonly ILogger _logger;
        private readonly DbSet<T> _db;
        public Repository(AppDataContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _db = _context.Set<T>();
        }

        public async Task Delete(int id)
        {
            try
            {
                var entity = await _db.FindAsync(id);
                if (entity == null)
                {
                    _logger.LogInformation($"[{typeof(T)}/Delete] Invalid Entity ID");
                    return;
                }
                _db.Remove(entity);
                _logger.LogInformation($"[{typeof(T)}/Delete] Invalid Entity ID");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{typeof(T)}/Delete] Exception Occurred while Delete");
            }
            
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            try
            {
                _db.RemoveRange(entities);
                _logger.LogInformation($"[{typeof(T)}/DeleteRange] Deleted {string.Join(",", entities)}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{typeof(T)}/DeleteRange] Exception Occurred while DeleteRange");
            }            
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression = null, List<string> includes = null)
        {
            try
            {
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        _db.Include(include);
                    }
                }

                var result = await _db.AsNoTracking().FirstOrDefaultAsync(expression);
                _logger.LogInformation($"[{typeof(T)}/Get] Get {string.Join(",", result)}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{typeof(T)}/Get] Exception Occurred while Get");
                return null;
            }            
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, List<string> includes = null)
        {
            try
            {
                IQueryable<T> queryable = _db;
                if (expression != null)
                { 
                    queryable = queryable.Where(expression);
                }

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        _db.Include(include);
                    }
                }

                var ordered = orderby?.Invoke(queryable);

                var result = await ordered.AsNoTracking().ToListAsync();
                _logger.LogInformation($"[{typeof(T)}/GetAll] GetAll {string.Join(",", result)}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{typeof(T)}/GetAll] Exception Occurred while GetAll");
                return null;
            }
        }

        public async Task Insert(T entity)
        {
            try
            {
                var result = await _db.AddAsync(entity);
                if(result.State == EntityState.Added)
                {
                    _logger.LogInformation($"[{typeof(T)}/Insert] Inserted {entity}");
                }                
                else
                {
                    _logger.LogInformation($"[{typeof(T)}/Insert] Insert Failed {entity} Entity State {result.State}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{typeof(T)}/Insert] Exception Occurred while Insert");
            }            
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            try
            {
                await _db.AddRangeAsync(entities);
                _logger.LogInformation($"[{typeof(T)}/InsertRange] InsertRange {string.Join(",", entities)}");          
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{typeof(T)}/InsertRange] Exception Occurred while InsertRange {string.Join(",", entities)}");
            }
        }

        public void Update(T entity)
        {
            try
            {
                _db.Attach(entity);
                _logger.LogInformation($"[{typeof(T)}/Update] Update {entity}");
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{typeof(T)}/Update] Exception Occurred while Update {entity}");
            }
        }
    }

}
