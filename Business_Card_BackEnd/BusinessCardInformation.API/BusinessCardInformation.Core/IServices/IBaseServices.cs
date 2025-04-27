

using BusinessCardInformation.Core.Models.Request;

namespace BusinessCardInformation.Core.IServices
{
    public interface IBaseServices<T, TFilter> where T : class where TFilter : class
    {
        Task<T> GetById(int id);
        Task<bool> Delete(int id);
        Task<PageResult<T>> GetAll(TFilter filter); 
        Task<T> Create(T entity);
        Task<List<T>> CreateBulk(List<T> entity);
        Task Update(T entity); 
    }
}
