using AutoMapper;
using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Core.Models.Request;

namespace BusinessCardInformation.Infra.Services
{
    public class BaseServices<TEntity, TModel> : IBaseServices<TModel> where TEntity : class where TModel : class
    {
        readonly IBaseRepository<TEntity> _repository;
        private readonly IMapper _mapper;


        public BaseServices(IBaseRepository<TEntity> repository, IMapper mapper)
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

        public async Task<ModelBaseFilter<TModel>> GetAll(ModelBaseFilter<TModel> filter)
        {
            var filterEntity = new ModelBaseFilter<TEntity>();
            filterEntity.PageSize = filter.PageSize; ;
            filterEntity.PageIndex = filter.PageIndex;
            var query = await _repository.GetAll(filterEntity);
            filter.Collection = _mapper.Map<List<TModel>>(query.Collection);

            return filter;
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
