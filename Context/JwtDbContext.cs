using Microsoft.EntityFrameworkCore;
using PratikJwt.Entity;

namespace PratikJwt.Context
{
    public class JwtDbContext : DbContext
    {
        public JwtDbContext(DbContextOptions<JwtDbContext> options) : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                        .Property(u => u.Email)
                        .IsRequired();

            modelBuilder.Entity<UserEntity>()
                        .Property(u => u.Password)
                        .IsRequired();

            modelBuilder.Entity<UserEntity>().HasData(new UserEntity
            {
                Id = 1,
                Email = "alaattin@example.com",
                Password = "12345"
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
