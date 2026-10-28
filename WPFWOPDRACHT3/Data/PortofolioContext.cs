using System;
using Microsoft.EntityFrameworkCore;
using WPFWOPDRACHT3.Models;

namespace WPFWOPDRACHT3.Data
{
    public class PortofolioContext(DbContextOptions<PortofolioContext> options) : DbContext (options)
    {
        public DbSet<BlogModel> Blogposts {get; set;}

        public DbSet<ProjectModel> Projecten {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Data Source=LAPTOP-KBDP7O51\SQLEXPRESS02;Initial Catalog=PortofolioDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;");
    }
    }
}
