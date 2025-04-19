using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;
using BusinessCardInformation.Core.Sheard;
using BusinessCardInformation.Infra.ApplicationDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebSite.Core.Repositorys
{
    public class BusinessCardRepo : BaseRepository<BusinessCard>, IBusinessCardRepository 
    { 
        //readonly RoleManager<Role> _identityRole;

        public BusinessCardRepo(AppDbContext dbContext) : base(dbContext) 
        { 
        } 

        //public async Task<BusinessCardDTO> Create(BusinessCardDTO role)
        //{
        //    var operationResult = new OperationResult<BusinessCardDTO>();
        //    var result = await _identityRole.CreateAsync(role);
        //    if (!result.Succeeded)
        //    {
        //        operationResult.Success = false;
        //        operationResult.Errors = result.Errors.Select(x => x.Description).ToList();
        //        return operationResult;
        //    }
        //    operationResult.Message = "Succeeded";
        //    operationResult.Success = true;
        //    return operationResult;
        //}

        //public async Task<List<BusinessCardDTO>> GetAll()
        //{
        //    var result = new OperationResult<List<RoleModule>>();
        //    var query = await _identityRole.Roles.Select(x => x.ToRoleModeule()).ToListAsync();

        //    result.Success = query.Any();
        //    result.Data = query;
        //    return result;

        //}
    }
}
