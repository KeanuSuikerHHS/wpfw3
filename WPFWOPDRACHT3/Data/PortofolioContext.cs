using System;
using Microsoft.EntityFrameworkCore;
using WPFWOPDRACHT3.Models;

namespace WPFWOPDRACHT3.Data
{
    public class PortofolioContext(DbContextOptions<PortofolioContext> options) : DbContext (options)
    {
        public DbSet<BlogModel> Blogposts {get; set;}

        public DbSet<ProjectModel> Projecten {get; set;}

    }
}
