using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        public PointOfInterestController(ILogger<PointOfInterestController> logger,
            IMailService mailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterest(int cityId)
        {
            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"logger infomation GetPointOfInterest {cityId}");
                return NotFound();
            }
            var pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterest));
        }
    

        [HttpGet("{pointsofinterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointsofinterestId)
        {

            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"logger infomation GetPointOfInterest {cityId}");
                return NotFound();
            }

            var pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointsofinterestId);

            if(pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterest));

            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var pointOfInterest = city.PointOfInterests.FirstOrDefault(c => c.Id == pointsofinterestId);
            //if (pointOfInterest == null)
            //{
            //    return NotFound();
            //}


        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(
            int cityId,
           PointOfInterestForCreationDto pointOfInterest)
        {

            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"logger infomation GetPointOfInterest {cityId}");
                return NotFound();
            }

            var finalPointOfInteres = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            
            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInteres);

            await _cityInfoRepository.SaveChangesAsync();

            var createPointOfInteresToReturn = _mapper.Map<Models.PointOfInterestDto>(finalPointOfInteres);
            return CreatedAtRoute("",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = createPointOfInteresToReturn.Id
                },
                createPointOfInteresToReturn);
        }


        [HttpPut("{pointsofinterestId}")]
        public ActionResult UpdatePointOfInterest(
            int cityId,
            int pointsofinterestId,
          PointOfInterestForUpdateDto pointOfInterest)

        {
            //find city
            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            //Console.WriteLine("City");
            //Console.WriteLine(city);

            //if (city == null)
            //{
            //    return NotFound();
            //}
            //var pointofinterest = city.PointOfInterests.FirstOrDefault(c => c.Id == pointsofinterestId);

            //if (pointofinterest == null)
            //{
            //    return NotFound();
            //}

            //pointofinterest.Name = pointOfInterest.Name;
            //pointofinterest.Description = pointOfInterest.Description!;
            return NoContent();
        }


        [HttpPatch("{pointsofinterestId}")]
        public ActionResult PartiallyUpdatePointOfInterest(
           int cityId,
           int pointsofinterestId,
         [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)

        {
            //find city
            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointsofinterestId);
            //if (pointOfInterestFromStore == null)
            //{
            //    return NotFound();
            //}

            //var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            //{
            //    Name = pointOfInterestFromStore.Name,
            //    Description = pointOfInterestFromStore.Description
            //};
            //patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            //pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }


        [HttpDelete("{pointsofinterestId}")]
        public ActionResult DeletePointOfInterest(
           int cityId,
           int pointsofinterestId)

        {
            //find city
            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            //if (city == null)
            //{
            //    return NotFound();
            //}
            //var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointsofinterestId);

            //if (pointOfInterestFromStore == null)
            //{
            //    return NotFound();
            //}

            //city.PointOfInterests.Remove(pointOfInterestFromStore);
            //_mailService.Send("Point of interest deleted", $"Point of interest {pointOfInterestFromStore.Name} with {pointOfInterestFromStore.Id} was deleted");
            return NoContent();
        }
    }
}