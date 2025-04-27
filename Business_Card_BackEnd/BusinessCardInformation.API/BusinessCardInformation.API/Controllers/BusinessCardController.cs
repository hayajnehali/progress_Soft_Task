using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml.Serialization;

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
            if (result)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        [Route("create-bulk")]
        public async Task<IActionResult> CreateBulk([FromBody] List<BusinessCardDTO> productModule)
        {
            // await _baseServices.Create(productModule);
            var result = await _baseServices.CreateBulk(productModule);
            if (result == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            string extension = Path.GetExtension(file.FileName).ToLower();

            using var reader = new StreamReader(file.OpenReadStream());
            string content = await reader.ReadToEndAsync();

            List<BusinessCardDTO> cards = extension switch
            {
                ".csv" => ParseCsv(content),
                ".xml" => ParseXml(content),
                _ => throw new Exception("Unsupported format")
            };

            return Ok(cards); // Or store/save to DB etc.
        }

        private List<BusinessCardDTO> ParseXml(string xmlContent)
        {
            var serializer = new XmlSerializer(typeof(List<BusinessCardDTO>), new XmlRootAttribute("BusinessCards"));
            using var reader = new StringReader(xmlContent);
            return (List<BusinessCardDTO>)serializer.Deserialize(reader);
        }

        private List<BusinessCardDTO> ParseCsv(string csvContent)
        {
            try
            {
                var records = new List<BusinessCardDTO>();
                using var reader = new StringReader(csvContent);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                records = csv.GetRecords<BusinessCardDTO>().ToList();
                return records;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
