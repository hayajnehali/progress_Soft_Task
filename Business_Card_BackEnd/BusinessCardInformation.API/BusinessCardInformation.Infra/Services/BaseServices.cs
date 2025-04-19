using AutoMapper;
using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Core.IServices;

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

        public async Task<List<TModel>> GetAll()
        {
            var query = await _repository.GetAll();
            return _mapper.Map<List<TModel>>(query); ;
        }
        public async Task<TModel> Create(TModel model)
        {
            var item = _mapper.Map<TEntity>(model); ;
          var entity=  await _repository.Create(item);

            return _mapper.Map<TModel>(entity); ;
        }
        public async Task Update(TModel entity)
        {
            //  await _repository.Update(entity);
            var item = _mapper.Map<TEntity>(entity); ;
            await _repository.Update(item);
        }
    }
}
