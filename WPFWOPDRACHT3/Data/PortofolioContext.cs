using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WPFWOPDRACHT3.Models;

namespace WPFWOPDRACHT3.Data
{
    public class PortofolioContext : IdentityDbContext<UserModel>
    {
         public PortofolioContext(DbContextOptions<PortofolioContext> options)
            : base(options)
        {
            
        }

        public DbSet<BlogModel> Blogposts {get; set;}

        public DbSet<ProjectModel> Projecten {get; set;}

    }
}
