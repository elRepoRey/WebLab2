namespace TheFlowerplace.Services
{
    /// <summary>
    /// Generic service for database operations.
    /// </summary>
    public interface ISQLService<T> where T : class
    {        
        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T entity);

     
        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }

   
}
