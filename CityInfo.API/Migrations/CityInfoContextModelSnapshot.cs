﻿// <auto-generated />
using CityInfo.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityInfo.API.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    partial class CityInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("CityInfo.API.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The one with that big park",
                            Name = "New york City"
                        },
                        new
                        {
                            Id = 2,
                            Description = "The one with that big park2",
                            Name = "New york City2"
                        },
                        new
                        {
                            Id = 3,
                            Description = "The one with that big park3",
                            Name = "New york City3"
                        });
                });

            modelBuilder.Entity("CityInfo.API.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointOfInterests");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Description = "The one with that big Central park",
                            Name = "Central park"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 2,
                            Description = "The one with that big Central park2",
                            Name = "Central park2"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 3,
                            Description = "The one with that big Central park3",
                            Name = "Central park3"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 1,
                            Description = "The one with that big Central park4",
                            Name = "Central park4"
                        });
                });

            modelBuilder.Entity("CityInfo.API.Entities.PointOfInterest", b =>
                {
                    b.HasOne("CityInfo.API.Entities.City", "City")
                        .WithMany("PointOfInterests")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CityInfo.API.Entities.City", b =>
                {
                    b.Navigation("PointOfInterests");
                });
#pragma warning restore 612, 618
        }
    }
}
