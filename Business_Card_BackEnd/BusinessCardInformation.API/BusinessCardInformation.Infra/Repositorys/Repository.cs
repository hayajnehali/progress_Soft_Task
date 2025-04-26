

using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Infra.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BusinessCardInformation.Core.Repositorys
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        readonly AppDbContext _dbContext;
        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<T> GetById(int id)
        {
            T result = await _dbContext.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<ModelBaseFilter<T>> GetAll(ModelBaseFilter<T> modelBaseFilter)
        {
            var filter = new ModelBaseFilter<T>();
            filter.Collection = await _dbContext.Set<T>().ToListAsync();
            return filter;
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
        public async Task<List<T>> CreateBulk(List<T> entities)
        {
            try
            {
                _dbContext.Set<T>().AddRange(entities);
                await _dbContext.SaveChangesAsync();
                return entities;
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
