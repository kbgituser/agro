﻿using Agro.Model.Entities;
using Agro.Model.WebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agro.Model.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<LoginModel>? LoginModels { get; set; }
        public DbSet<Intention> Intentions { get; set; }
        public DbSet<Request> Requests { get; set; }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Mall> Malls { get; set; }
        public DbSet<MallPhoto> MallPhotos { get; set; }
        public DbSet<Premise> Premises { get; set; }
        public DbSet<PremisePhoto> PremisePhotos { get; set; }
        public DbSet<PremiseType> PremiseTypes { get; set; }
        public DbSet<MeatType> MeatTypes { get; set; }

        public DbSet<Advertisement> Advertisements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //SeedUsers(builder);
            //SeedRoles(builder);
            //SeedUserRoles(builder);

            builder
                .Entity<LoginModel>()
                .HasData(
                    new LoginModel
                    {
                        Id = 1,
                        UserName = "johndoe",
                        Password = "def@123"
                    }
                );

            builder
                .Entity<Intention>()
                .HasOne(x => x.Request)
                .WithMany(x => x.Intentions)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Entity<Intention>()
                .HasOne(r => r.City)
                .WithMany(c => c.Requests)
                .HasForeignKey(o => o.CityId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildrenCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Premise>()
                .HasOne(c => c.Mall)
                .WithMany(c => c.Premises)
                .HasForeignKey(c => c.MallId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .Entity<Mall>()
                .HasOne(x => x.City)
                .WithMany()
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Entity<Premise>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        //private void SeedUsers(ModelBuilder builder)
        //{
        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        UserName = "Admin",
        //        Email = "admin@ad.me"
        //    };
        //    //var passwordHash = _userManager.PasswordHasher.HashPassword(user, "Test");
        //    //user.PasswordHash = passwordHash;

        //    builder.Entity<ApplicationUser>().HasData(user);
        //}

        //private void SeedRoles(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityRole>().HasData(
        //        new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" }
        //        );
        //}

        //private void SeedUserRoles(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityUserRole<string>>().HasData(
        //        new IdentityUserRole<string>() { RoleId = "d0bcaf79-27ed-43f9-a531-55ae2bcb3053", UserId = "413eee48-aad2-42a1-813b-43a0cab467a8" }
        //        );
        //}
    }
}
