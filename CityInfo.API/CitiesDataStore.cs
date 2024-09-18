using CityInfo.API.Models;
using System.Xml.Linq;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        //public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {

            // init dummy data

            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test",
                    PointOfInterests = {
                    new PointOfInterestDto()
                    {
                        Id= 1,
                        Name = "TestPoint1",
                        Description = "TestPoint1",
                    },
                    new PointOfInterestDto()
                    {
                        Id= 11,
                        Name = "TestPoint11",
                        Description = "TestPoint11",
                    }
                    }

                },
                 new CityDto()
                {
                    Id = 2,
                    Name = "Test2",
                    Description = "Test2",
                      PointOfInterests = {
                    new PointOfInterestDto()
                    {
                        Id= 2,
                        Name = "TestPoint2",
                        Description = "TestPoint2",
                    },
                    new PointOfInterestDto()
                    {
                        Id= 22,
                        Name = "TestPoint22",
                        Description = "TestPoint22",
                    }
                    }
                },
                  new CityDto()
                {
                    Id = 3,
                    Name = "Test3",
                    Description = "Test3",
                      PointOfInterests = {
                    new PointOfInterestDto()
                    {
                        Id= 1,
                        Name = "TestPoint3",
                        Description = "TestPoint3",
                    },
                    new PointOfInterestDto()
                    {
                        Id= 33,
                        Name = "TestPoint33",
                        Description = "TestPoint33",
                    }
                    }
                }
            };
        }
    }
}
