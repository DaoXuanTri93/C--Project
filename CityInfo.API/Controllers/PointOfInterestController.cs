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
        private readonly CitiesDataStore _citiesDataStore;

        public PointOfInterestController(ILogger<PointOfInterestController> logger, 
            IMailService mailService,
            CitiesDataStore citiesDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointOfInterest(int cityId)
        {
            try
            {
                //throw new Exception("alo");

                //find city
                var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} not found !!!");
                    return NotFound();
                }

                return Ok(city.PointOfInterests);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting points of interest for city with id {cityId}.",
                    ex);
                return StatusCode(500, "A problem happend while hanlding your request");
            }
        }

        [HttpGet("{pointsofinterestId}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointsofinterestId)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointOfInterests.FirstOrDefault(c => c.Id == pointsofinterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(
            int cityId,
           PointOfInterestForCreationDto pointOfInterest)
        {
            //find city
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            //demo purposes -to be improved

            var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(
                c => c.PointOfInterests).Max(p => p.Id);


            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description!
            };

            city.PointOfInterests.Add(finalPointOfInterest);
            //return Ok(city);
            return CreatedAtRoute("",
                new
                {
                    Id = cityId,
                    pointOfInterestId = finalPointOfInterest.Id
                },
                finalPointOfInterest);
        }


        [HttpPut("{pointsofinterestId}")]
        public ActionResult UpdatePointOfInterest(
            int cityId,
            int pointsofinterestId,
          PointOfInterestForUpdateDto pointOfInterest)

        {
            //find city
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            Console.WriteLine("City");
            Console.WriteLine(city);

            if (city == null)
            {
                return NotFound();
            }
            var pointofinterest = city.PointOfInterests.FirstOrDefault(c => c.Id == pointsofinterestId);

            if (pointofinterest == null)
            {
                return NotFound();
            }

            pointofinterest.Name = pointOfInterest.Name;
            pointofinterest.Description = pointOfInterest.Description!;
            return NoContent();
        }


        [HttpPatch("{pointsofinterestId}")]
        public ActionResult PartiallyUpdatePointOfInterest(
           int cityId,
           int pointsofinterestId,
         [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)

        {
            //find city
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointsofinterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }


        [HttpDelete("{pointsofinterestId}")]
        public ActionResult DeletePointOfInterest(
           int cityId,
           int pointsofinterestId)

        {
            //find city
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointsofinterestId);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointOfInterests.Remove(pointOfInterestFromStore);
            _mailService.Send("Point of interest deleted", $"Point of interest {pointOfInterestFromStore.Name} with {pointOfInterestFromStore.Id} was deleted");
            return NoContent();
        }
    }
}