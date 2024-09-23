using AutoMapper;
using CityInfo.API.Entities;
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
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
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

            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));

        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
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
        public async Task<ActionResult> UpdatePointOfInterest(
            int cityId,
            int pointsofinterestId,
          PointOfInterestForUpdateDto pointOfInterest)

        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"logger infomation GetPointOfInterest {cityId}");
                return NotFound();
            }


            //IEnumerable<PointOfInterest?>
            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointsofinterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();
           
            return NoContent();
        }


        [HttpPatch("{pointsofinterestId}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(
           int cityId,
           int pointsofinterestId,
         [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)

        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"logger infomation GetPointOfInterest {cityId}");
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointsofinterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{pointsofinterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(
           int cityId,
           int pointsofinterestId)

        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"logger infomation GetPointOfInterest {cityId}");
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointsofinterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

             _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();
            _mailService.Send("Point of interest deleted", $"Point of interest {pointOfInterestEntity.Name} with {pointOfInterestEntity.Id} was deleted");
            return NoContent();
        }
    }
}