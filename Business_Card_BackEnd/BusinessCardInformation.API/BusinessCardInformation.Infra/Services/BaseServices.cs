using AutoMapper;
using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Core.Models.Request;

namespace BusinessCardInformation.Infra.Services
{
    public class BaseServices<TEntity, TModel, TFilter> : IBaseServices<TModel, TFilter> where TEntity : class where TModel : class where TFilter : class
    {
        readonly IBaseRepository<TEntity, TFilter> _repository;
        private readonly IMapper _mapper;


        public BaseServices(IBaseRepository<TEntity, TFilter> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TModel> GetById(int id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<TModel>(entity);
        }
        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<PageResult<TModel>> GetAll(TFilter filter)
        { 
            var result = new PageResult<TModel>(); 
            var query = await _repository.GetAll(filter);
            result.Collection = _mapper.Map<List<TModel>>(query.Collection); 
            result.TotalNumberOf =query.TotalNumberOf; 
            return result;
        }
        public async Task<TModel> Create(TModel model)
        {
            var item = _mapper.Map<TEntity>(model); ;
            var entity = await _repository.Create(item);

            return _mapper.Map<TModel>(entity); ;
        }
        public async Task<List<TModel>> CreateBulk(List<TModel> model)
        {
            var item = _mapper.Map<List<TEntity>>(model); ;
            var entity = await _repository.CreateBulk(item); 
            return _mapper.Map<List<TModel>>(entity); ;
        }
        public async Task Update(TModel entity)
        {
            var item = _mapper.Map<TEntity>(entity); ;
            await _repository.Update(item);
        }
    }
}
