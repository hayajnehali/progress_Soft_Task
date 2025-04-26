using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;
using Microsoft.AspNetCore.Identity; 

namespace BusinessCardInformation.Core.IRepositorys
{
    public interface IBusinessCardRepository :IBaseRepository<BusinessCard,BusinessCardFilter>
    { 
    }
     
}
