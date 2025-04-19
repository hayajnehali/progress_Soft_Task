using AutoMapper;
using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;

namespace BusinessCardInformation.Infra.Services
{
    public class BusinessCardServices: BaseServices<BusinessCard, BusinessCardDTO>, IBusinessCardServices
    {
        //readonly IRoleRepository _roleRepository;


        public BusinessCardServices(IBaseRepository<BusinessCard> repository, IMapper mapper) : base(repository,mapper)
        {
        }
        //public RoleServices(IRoleRepository repository)
        //{
        //    _roleRepository = repository;
        //}
        //public async Task<OperationResult<RoleModule>> Create(RoleModule roleModule)
        //{

        // return  await _roleRepository.Create(roleModule.ToRoleEntity());

        //}

        //public async Task<OperationResult<List<RoleModule>>> GetAll()
        //{
        //    return await _roleRepository.GetAll();
        //}
    }
}
