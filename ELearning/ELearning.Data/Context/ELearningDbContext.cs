using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using ELearning.Data.Entities;
using ELearning.Data.Configurations;

namespace ELearning.Data.Context
{
    public class ELearningDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                .AddJsonFile("appsettings.json")
                                                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ELearningDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());

            //Seeding data
            modelBuilder.Entity<Student>().HasData(
                new Student() {Id = 1, Email = "minhtan@email.com", Password = "abc"},
                new Student() {Id = 2, Email = "nhatlinh@gmail.com", Password = "123"}
                );

            modelBuilder.Entity<Course>().HasData(
                new Course() { Id = 1, Name = "Introduction to Machine Learning", Description = "Basic concept about ML", Price = 19.99M, IsProgressLimited = true}
                );
        }
    }
}
