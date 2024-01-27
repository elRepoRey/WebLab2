using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheFlowerplace.Data;

namespace TheFlowerplace.Services
{
  
    public class SQLService<T> : ISQLService<T> where T : class

    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

       
        public SQLService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
      
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
       
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
               
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
               
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
