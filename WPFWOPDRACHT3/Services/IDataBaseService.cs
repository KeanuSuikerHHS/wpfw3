using System;
using WPFWOPDRACHT3.Models;

namespace WPFWOPDRACHT3.Services
{
    public interface IDataBaseService
    {
        //Blog services
        Task<IEnumerable<BlogModel>> GetAllBlogPosts();
        Task<BlogModel> GetBlogById (int id);
        Task<BlogModel> AddBlogPost(BlogModel blogmodel);
        Task<bool> UpdateBlogPost(int id, BlogModel blogModel);
        Task<bool> DeleteBlogPost(int id);

        //Project services
        Task<IEnumerable<ProjectModel>> GetAllProjecten();
        Task<ProjectModel> GetProjectById(int id);
        Task<ProjectModel> AddProject(ProjectModel projectModel);
        Task<bool> UpdateProject(int id, ProjectModel projectModel);
        Task<bool> DeleteProject(int id);
    }
}
