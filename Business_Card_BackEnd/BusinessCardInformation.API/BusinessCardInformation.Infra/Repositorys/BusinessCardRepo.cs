using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;
using BusinessCardInformation.Core.Repositorys; 
using BusinessCardInformation.Infra.ApplicationDbContext; 

namespace BusinessCardInformation.Core.Repositorys
{
    public class BusinessCardRepo : BaseRepository<BusinessCard>, IBusinessCardRepository 
    {  

        public BusinessCardRepo(AppDbContext dbContext) : base(dbContext) 
        { 
        } 
         
    }
}
