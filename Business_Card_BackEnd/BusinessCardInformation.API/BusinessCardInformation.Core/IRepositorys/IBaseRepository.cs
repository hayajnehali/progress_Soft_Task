

namespace BusinessCardInformation.Core.IRepositorys
{
    public interface IBaseRepository<T> where T : class
    {

        Task<T?> GetById(int id);
        Task<List<T>> GetAll();
        IQueryable<T> Query();
        Task<T> Create(T entity);
        Task Update(T entity);
        Task SaveChangesAsync();
    }
}
