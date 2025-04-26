

using BusinessCardInformation.Core.Models.Request;

namespace BusinessCardInformation.Core.IRepositorys
{
    public interface IBaseRepository<T> where T : class 
    {

        Task<T?> GetById(int id);
        Task<bool> Delete(int id);
        Task<ModelBaseFilter<T>> GetAll(ModelBaseFilter<T> filter);
        IQueryable<T> Query();
        Task<T> Create(T entity);
        Task<List<T>> CreateBulk(List<T> entity);
        Task Update(T entity);
        Task SaveChangesAsync();
    }
}
