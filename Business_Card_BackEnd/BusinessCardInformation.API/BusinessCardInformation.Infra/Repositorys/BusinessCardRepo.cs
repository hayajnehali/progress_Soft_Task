using AutoMapper;
using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;
using BusinessCardInformation.Core.Repositorys; 
using BusinessCardInformation.Infra.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace BusinessCardInformation.Core.Repositorys
{
    public class BusinessCardRepo : BaseRepository<BusinessCard,BusinessCardFilter>, IBusinessCardRepository 
    {   
        public BusinessCardRepo(AppDbContext dbContext) : base(dbContext){}

        public override async Task<PageResult<BusinessCard>> GetAll(BusinessCardFilter filter)
        {
            var query = Query(); 

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrWhiteSpace(filter.Gender))
            {
                query = query.Where(x => x.Gender == filter.Gender);
            }

            if (filter.DateOfBirth.HasValue)
            {
                var targetDate = filter.DateOfBirth.Value.Date;
                var nextDate = targetDate.AddDays(1);

                query = query.Where(x => x.DateOfBirth.Date == nextDate.Date );

            }

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                query = query.Where(x => x.Email.Contains(filter.Email));
            }

            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                query = query.Where(x => x.Phone.Contains(filter.Phone));
            }

            int skip = (filter.PageIndex - 1) * filter.PageSize;

            var result = new PageResult<BusinessCard>
            {
                TotalNumberOf = await query.CountAsync(),
                Collection = await query.Skip(skip).Take(filter.PageSize).ToListAsync()
            };
            return result;
        }

    }
}
