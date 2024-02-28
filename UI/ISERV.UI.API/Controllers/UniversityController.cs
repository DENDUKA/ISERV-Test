using ISERV.Persistence.EF.Services;
using ISERV.UI.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ISERV.UI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        public UniversitiesRepository _universitiesRepository;
        public UniversityController(UniversitiesRepository universitiesRepository) 
        {
            _universitiesRepository = universitiesRepository;
        }

        [HttpGet("search")]
        public async Task<ActionResult<GetUniversitiesResponce>> Get(string country, string universityName)
        {
            var result = await _universitiesRepository.Find(country, universityName);

            return Ok(result);
        }
    }
}
