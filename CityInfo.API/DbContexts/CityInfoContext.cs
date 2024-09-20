

using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterests { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("New york City")
                {
                    Id = 1,
                    Description = "The one with that big park"
                },
                new City("New york City2")
                {
                    Id = 2,
                    Description = "The one with that big park2"
                },
                new City("New york City3")
                {
                    Id = 3,
                    Description = "The one with that big park3"
                });

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("Central park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "The one with that big Central park"
                },
                new PointOfInterest("Central park2")
                {
                    Id = 2,
                    CityId = 2,
                    Description = "The one with that big Central park2"
                },
                new PointOfInterest("Central park3")
                {
                    Id = 3,
                    CityId = 3,
                    Description = "The one with that big Central park3"
                },
                 new PointOfInterest("Central park4")
                 {
                     Id = 4,
                     CityId = 1,
                     Description = "The one with that big Central park4"
                 }
                );
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlite("connectionstring");
        //     base.OnConfiguring(optionsBuilder);
        // }
    }
}
