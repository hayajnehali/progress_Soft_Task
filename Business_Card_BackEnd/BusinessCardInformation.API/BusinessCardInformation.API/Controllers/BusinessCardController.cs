using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using System.Xml;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 
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

            return Ok(cards); 
        }


         
        // Export data as CSV
        [HttpGet("export/csv")]
        public async Task<IActionResult> ExportToCSV([FromQuery] BusinessCardFilter businessCardFilter)
        {
            // Validate the filter if necessary
            if (businessCardFilter == null)
            {
                return BadRequest("Invalid filter parameters.");
            }
            try
            {

                var result = await _baseServices.GetAll(businessCardFilter);
                var businessCards = result.Collection;
                if (businessCards.Count == 0)
                    return NoContent();
                var csvBuilder = new StringBuilder();
                var header = "BusinessCardId,Name,Gender,DateOfBirth,Email,Phone,Photo,Address";
                csvBuilder.AppendLine(header);

                foreach (var card in businessCards)
                {
                    csvBuilder.AppendLine($"{card.BusinessCardId},{card.Name},{card.Gender},{card.DateOfBirth:yyyy-MM-dd},{card.Email},{card.Phone},{card.Photo},{card.Address}");
                }

                var csvContent = csvBuilder.ToString();
                var byteArray = Encoding.UTF8.GetBytes(csvContent);
                var stream = new MemoryStream(byteArray);
                Response.Headers.Add("Content-Disposition", "attachment; filename=business_cards.csv");
                return File(stream, "text/csv");
            }
            catch (Exception)
            { 
                return StatusCode(500, "An error occurred while processing your request.");

                throw;
            }

        }
         
        // Export data as XML
        [HttpGet("export/xml")]
        public async Task<IActionResult> ExportToXML([FromQuery] BusinessCardFilter businessCardFilter)
        {
            try
            { 
                var result = await _baseServices.GetAll(businessCardFilter);
                var businessCards = result.Collection; 
                var xmlRoot = new XmlRootAttribute("BusinessCards");
                var xmlSerializer = new XmlSerializer(typeof(List<BusinessCardDTO>), xmlRoot);

                using var stringWriter = new StringWriter();
                using var xmlWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented }; 
                xmlSerializer.Serialize(xmlWriter, businessCards);

                var xmlContent = stringWriter.ToString();
                var byteArray = Encoding.UTF8.GetBytes(xmlContent);
                var stream = new MemoryStream(byteArray); 
                Response.Headers.Add("Content-Disposition", "attachment; filename=business_cards.xml");
                return File(stream, "application/xml");
            }
            catch (Exception ex)
            {

                throw;
            }

        } 

        private List<BusinessCardDTO> ParseXml(string xmlContent)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<BusinessCardDTO>), new XmlRootAttribute("BusinessCards"));
                using var reader = new StringReader(xmlContent);
                var recordes= (List<BusinessCardDTO>)serializer.Deserialize(reader);
                return recordes;
            }
            catch (Exception ex)
            {

                throw;
            }
         
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
