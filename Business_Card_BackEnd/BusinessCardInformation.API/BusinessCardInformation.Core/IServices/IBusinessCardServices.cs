
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;

namespace BusinessCardInformation.Core.IServices
{ 
    public interface IBusinessCardServices:IBaseServices<BusinessCardDTO>
    {
        //Task<Role> GetById(Guid id);
        //Task<Role> GetByName(string name);
        //Task<OperationResult<RoleModule>> Create(RoleModule roleModule);
        //Task<OperationResult<List<RoleModule>>> GetAll();
    }
}
