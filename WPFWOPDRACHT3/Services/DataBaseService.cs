using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using WPFWOPDRACHT3.Data;
using WPFWOPDRACHT3.Models;

namespace WPFWOPDRACHT3.Services
{
    public class DataBaseService : IDataBaseService
    {
        private readonly PortofolioContext _context;

        public DataBaseService(PortofolioContext context)
        {
            _context = context;
        }

        public async Task<BlogModel> AddBlogPost(BlogModel blogmodel)
        {
            _context.Blogposts.Add(blogmodel);
            await _context.SaveChangesAsync();
            return blogmodel;
        }

        public async Task<ProjectModel> AddProject(ProjectModel projectModel)
        {
            _context.Projecten.Add(projectModel);
            await _context.SaveChangesAsync();
            return projectModel;
        }

        public async Task<bool> DeleteBlogPost(int id)
        {
            var blogPost = await _context.Blogposts.FindAsync(id);
            _context.Blogposts.Remove(blogPost);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProject(int id)
        {
            var project = await _context.Projecten.FindAsync(id);
            _context.Projecten.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BlogModel>> GetAllBlogPosts()
        {
            return await _context.Blogposts.ToListAsync();
        }

        public async Task<IEnumerable<ProjectModel>> GetAllProjecten()
        {
            return await _context.Projecten.ToListAsync();
        }

        public async Task<BlogModel> GetBlogById(int id)
        {
            return await _context.Blogposts.FindAsync(id);
        }

        public async Task<ProjectModel> GetProjectById(int id)
        {
            return await _context.Projecten.FindAsync(id);
        }

        public async Task<bool> UpdateBlogPost(int id, BlogModel blogModel)
        {
            var blog = await _context.Blogposts.FindAsync(id);
            _context.Entry(blog).CurrentValues.SetValues(blogModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProject(int id, ProjectModel projectModel)
        {
            var project = await _context.Projecten.FindAsync(id);
            _context.Entry(project).CurrentValues.SetValues(projectModel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
