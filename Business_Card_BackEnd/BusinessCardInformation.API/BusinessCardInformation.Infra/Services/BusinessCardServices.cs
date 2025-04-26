using AutoMapper;
using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;
using static Dapper.SqlMapper;

namespace BusinessCardInformation.Infra.Services
{
    public class BusinessCardServices: BaseServices<BusinessCard, BusinessCardDTO,BusinessCardFilter>, IBusinessCardServices
    { 
        public BusinessCardServices(IBaseRepository<BusinessCard,BusinessCardFilter> repository, IMapper mapper) : base(repository,mapper){} 
    }
}
