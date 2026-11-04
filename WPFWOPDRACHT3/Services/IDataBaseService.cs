using System;
using WPFWOPDRACHT3.Dtos.BlogDTOs;
using WPFWOPDRACHT3.Dtos.ProjectDTOs;
using WPFWOPDRACHT3.Models;

namespace WPFWOPDRACHT3.Services
{
    public interface IDataBaseService
    {
        //Blog services
        Task<IEnumerable<GetBlogDTO>> GetAllBlogPosts();
        Task<GetBlogDTO> GetBlogById (int id);
        Task<CreateBlogDTO> AddBlogPost(BlogModel blogmodel);
        Task<UpdateBlogDTO> UpdateBlogPost(int id, BlogModel blogModel);
        Task<bool> DeleteBlogPost(int id);

        //Project services
        Task<IEnumerable<GetProjectDTO>> GetAllProjecten();
        Task<GetProjectDTO> GetProjectById(int id);
        Task<CreateProjectDTO> AddProject(ProjectModel projectModel);
        Task<UpdateProjectDTO> UpdateProject(int id, ProjectModel projectModel);
        Task<bool> DeleteProject(int id);
    }
}
