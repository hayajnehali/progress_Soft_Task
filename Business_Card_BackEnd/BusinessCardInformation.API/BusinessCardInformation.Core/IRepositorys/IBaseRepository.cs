

using BusinessCardInformation.Core.Models.Request;

namespace BusinessCardInformation.Core.IRepositorys
{
    public interface IBaseRepository<T, TFilter> where T : class  where TFilter : class 
    {

        Task<T?> GetById(int id);
        Task<bool> Delete(int id);
        Task<PageResult<T>> GetAll(TFilter filter);
        IQueryable<T> Query();
        Task<T> Create(T entity);
        Task<List<T>> CreateBulk(List<T> entity);
        Task Update(T entity);
        Task SaveChangesAsync();
    }
}
