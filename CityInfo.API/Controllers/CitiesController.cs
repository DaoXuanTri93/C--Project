using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper) {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }   

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
             var cityEntities = await _cityInfoRepository.GetCitiesAsync();

            //var results = new List<CityWithoutPointOfInterestDto>();
            //foreach (var city in cityEntities)
            //{
            //    results.Add(new CityWithoutPointOfInterestDto
            //    {
            //        Id = city.Id,
            //        Name = city.Name,
            //        Description = city.Description
            //    });
            //}


            return Ok(_mapper.Map<IEnumerable<CityDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool includePointOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if(includePointOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));
   
        }
    }
}
