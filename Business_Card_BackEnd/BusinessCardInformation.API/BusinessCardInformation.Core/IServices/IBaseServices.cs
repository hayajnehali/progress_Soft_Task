

using BusinessCardInformation.Core.Models.Request;

namespace BusinessCardInformation.Core.IServices
{
    public interface IBaseServices<T> where T : class
    {
        Task<T> GetById(int id);
        Task<bool> Delete(int id);
        Task<ModelBaseFilter<T>> GetAll(ModelBaseFilter<T> filter); 
        Task<T> Create(T entity);
        Task<List<T>> CreateBulk(List<T> entity);
        Task Update(T entity); 
    }
}
