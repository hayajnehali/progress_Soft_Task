using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Core.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 

namespace ResturantWebSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class BusinessCardController : ControllerBase
    { 
        readonly IBusinessCardServices _baseServices;
        public BusinessCardController(IBusinessCardServices baseServices)
        { 
            _baseServices = baseServices;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] BusinessCardDTO productModule)
        {
           // await _baseServices.Create(productModule);
            var result = await _baseServices.Create(productModule);
            if (result ==null) 
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] BusinessCardDTO businessCardDTO)
        { 
            await _baseServices.Update(businessCardDTO);
            return Ok();
        }


        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] BusinessCardFilter businessCardFilter)
        {
          var result= await _baseServices.GetAll(businessCardFilter);
            return Ok(result);
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var item = await _baseServices.GetById(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _baseServices.Delete(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
