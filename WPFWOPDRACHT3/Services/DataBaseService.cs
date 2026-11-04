using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPFWOPDRACHT3.Data;
using WPFWOPDRACHT3.Dtos.BlogDTOs;
using WPFWOPDRACHT3.Dtos.ProjectDTOs;
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

        public async Task<CreateBlogDTO> AddBlogPost(BlogModel blogmodel)
        {
            _context.Blogposts.Add(blogmodel);
            await _context.SaveChangesAsync();
            return new CreateBlogDTO(blogmodel.Titel, blogmodel.Bericht, blogmodel.Datum);
        }

        public async Task<CreateProjectDTO> AddProject(ProjectModel projectModel)
        {
            _context.Projecten.Add(projectModel);
            await _context.SaveChangesAsync();
            return new CreateProjectDTO(projectModel.Titel, projectModel.Beschrijving, projectModel.Categorie, projectModel.GitHubURL, projectModel.Datum);
        }

        public async Task<bool> DeleteBlogPost(int id)
        {
            var blogPost = await _context.Blogposts.FindAsync(id);
            if (blogPost == null) return false;

            _context.Blogposts.Remove(blogPost);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProject(int id)
        {
            var project = await _context.Projecten.FindAsync(id);
            if (project == null) return false;

            _context.Projecten.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<GetBlogDTO>> GetAllBlogPosts()
        {
            return await _context.Blogposts
                .Select(blog => new GetBlogDTO(blog.Id, blog.Titel, blog.Bericht, blog.Datum))
                .ToListAsync();
        }

        public async Task<IEnumerable<GetProjectDTO>> GetAllProjecten()
        {
            return await _context.Projecten
                .Select(project => new GetProjectDTO(project.Id, project.Titel, project.Beschrijving, project.Categorie, project.GitHubURL, project.Datum))
                .ToListAsync();
        }

        public async Task<GetBlogDTO> GetBlogById(int id)
        {
            var blog = await _context.Blogposts.FindAsync(id);
            if (blog == null) return null;

            return new GetBlogDTO(blog.Id, blog.Titel, blog.Bericht, blog.Datum);
        }

        public async Task<GetProjectDTO> GetProjectById(int id)
        {
            var project = await _context.Projecten.FindAsync(id);
            if (project == null) return null;

            return new GetProjectDTO(project.Id, project.Titel, project.Beschrijving, project.Categorie, project.GitHubURL, project.Datum);
        }

        public async Task<UpdateBlogDTO> UpdateBlogPost(int id, BlogModel blogModel)
        {
            var blog = await _context.Blogposts.FindAsync(id);
            if (blog == null) return null;

            _context.Entry(blog).CurrentValues.SetValues(blogModel);
            await _context.SaveChangesAsync();
            return new UpdateBlogDTO(blog.Id, blog.Titel, blog.Bericht, blog.Datum);
        }

        public async Task<UpdateProjectDTO> UpdateProject(int id, ProjectModel projectModel)
        {
            var project = await _context.Projecten.FindAsync(id);
            if (project == null) return null;

            _context.Entry(project).CurrentValues.SetValues(projectModel);
            await _context.SaveChangesAsync();
            return new UpdateProjectDTO(project.Id, project.Titel, project.Beschrijving, project.Categorie, project.GitHubURL, project.Datum);
        }
    }
}