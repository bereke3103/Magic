using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "New Villa",
                    Details = "Details is good",
                    ImageUrl = "http://infozakon.kz/uploads/posts/2023-07/1688626858_1688535881_kz3_9824_2.jpg",
                    Occupancy = 5,
                    Sqft = 505,
                    Amenity = "",
                    Rate=200,
                    CreatedDate= DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Name = "New Villa",
                    Details = "Details is good",
                    ImageUrl = "http://infozakon.kz/uploads/posts/2023-07/1688626858_1688535881_kz3_9824_2.jpg",
                    Occupancy = 5,
                    Sqft = 505,
                    Amenity = "",
                    Rate=200,
                    CreatedDate= DateTime.Now
                },
                new Villa()
                {
                    Id = 3,
                    Name = "New Villa",
                    Details = "Details is good",
                    ImageUrl = "http://infozakon.kz/uploads/posts/2023-07/1688626858_1688535881_kz3_9824_2.jpg",
                    Occupancy = 5,
                    Sqft = 505,
                    Amenity = "",
                    Rate=200,
                    CreatedDate= DateTime.Now
                },
                new Villa()
                {
                    Id = 4,
                    Name = "New Villa",
                    Details = "Details is good",
                    ImageUrl = "http://infozakon.kz/uploads/posts/2023-07/1688626858_1688535881_kz3_9824_2.jpg",
                    Occupancy = 5,
                    Sqft = 505,
                    Amenity = "",
                    Rate=200,
                    CreatedDate= DateTime.Now
                },
                new Villa()
                {
                    Id = 5,
                    Name = "New Villa",
                    Details = "Details is good",
                    ImageUrl = "http://infozakon.kz/uploads/posts/2023-07/1688626858_1688535881_kz3_9824_2.jpg",
                    Occupancy = 5,
                    Sqft = 505,
                    Amenity = "",
                    Rate=200,
                    CreatedDate= DateTime.Now
                });
        }
    }
}
