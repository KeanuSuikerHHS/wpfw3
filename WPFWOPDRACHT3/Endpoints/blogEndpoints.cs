using System;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Mvc;
using WPFWOPDRACHT3.Dtos;
using WPFWOPDRACHT3.Models;
using WPFWOPDRACHT3.Services;
using WPFWOPDRACHT3.Dtos.BlogDTOs;

namespace WPFWOPDRACHT3.Endpoints
{
    public static class blogEndpoints
    {
        public static void MapBlogEndpoints(this WebApplication app)
        {

            var blogGroup = app.MapGroup("/blog").WithTags("Blogs");

            // GET alle blogs
            blogGroup.MapGet("/", async([FromServices] DataBaseService db) =>
            {
                var blogPosts = await db.GetAllBlogPosts();
                return blogPosts is not null ? Results.Ok(blogPosts) : Results.NoContent();
            });

            // GET 1 blog via id
            blogGroup.MapGet("/{id}", async ([FromServices] DataBaseService db, int id) =>
            {
                var blogPost = await db.GetBlogById(id);
                return blogPost is not null ? Results.Ok(blogPost) : Results.NotFound();
            });

            // POST 1 blog aanmaken
            blogGroup.MapPost("/", async ([FromServices] DataBaseService db, CreateBlogDTO blog) =>
            {
                BlogModel blogModel = new BlogModel
                {
                    Titel = blog.Titel,
                    Bericht = blog.Bericht,
                    Datum = blog.Datum,
                };

                await db.AddBlogPost(blogModel);
                return Results.Created($"/blog/{blogModel.Id}", blogModel);
            });

            // PUT Wijzig 1 blog
            blogGroup.MapPut("/{id}", async ([FromServices] DataBaseService db, int id, UpdateBlogDTO blog) =>
            {
                var existingBlog = await db.GetBlogById(id);

                if (existingBlog is null)
                    {
                        return Results.NotFound();
                    }

                var blogModel = new BlogModel 
                    {
                        Id = id,
                        Titel = blog.Titel,
                        Bericht = blog.Bericht,
                        Datum = blog.Datum
                    };

                var updatedBlog = await db.UpdateBlogPost(id, blogModel);

                return Results.Ok(updatedBlog);
});

            // DELETE Verwijder 1 blog
            blogGroup.MapDelete("/{id}", async ([FromServices] DataBaseService db, int id) =>
            {
                var existingBlog = await db.GetBlogById(id);
    
                if (existingBlog is null)
                {
                    return Results.NotFound();
                }


                await db.DeleteBlogPost(id);

                return Results.NoContent();
            });
        }
    }
}
