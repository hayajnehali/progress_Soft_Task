

namespace BusinessCardInformation.Core.IServices
{
    public interface IBaseServices<T>
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll(); 
        Task<T> Create(T entity);
        Task Update(T entity); 
    }
}
