using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Contexts;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Interfaces;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Managers
{
    /// <inheritdoc cref="IRepositoryManager<T>"/>
    [ExcludeFromCodeCoverage]
    public class RepositoryManager<T> : IRepositoryManager<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _context;

        public RepositoryManager(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking(); //TODO
        }
        public IQueryable<T> GetAllAsTracking()
        {
            return _dbSet.AsTracking(); //TODO
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetEntityWithoutTrackingAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
