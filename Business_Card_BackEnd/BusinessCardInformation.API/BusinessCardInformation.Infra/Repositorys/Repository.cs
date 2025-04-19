

using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Infra.ApplicationDbContext;
using Microsoft.EntityFrameworkCore; 
using System.Linq.Expressions;

namespace ResturantWebSite.Core.Repositorys
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        readonly AppDbContext _dbContext; 
        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetById(int id)
        {
            T result = await _dbContext.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            try
            {
                _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
