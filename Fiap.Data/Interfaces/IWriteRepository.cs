namespace Fiap.Data.Interfaces
{
    public interface IWriteRepository<T> where T : class
    {
        void Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
    }
}