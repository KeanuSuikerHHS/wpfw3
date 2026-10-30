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

            // GET alle blogs
            app.MapGet("/blog", async([FromServices] DataBaseService db) =>
            {
                var blogPosts = await db.GetAllBlogPosts();
                return blogPosts is not null ? Results.Ok(blogPosts) : Results.NoContent();
            });

            // GET 1 blog via id
            app.MapGet("/blog/{id}", async ([FromServices] DataBaseService db, int id) =>
            {
                var blogPost = await db.GetBlogById(id);
                return blogPost is not null ? Results.Ok(blogPost) : Results.NotFound();
            });

            // POST 1 blog aanmaken
            app.MapPost("/blog", async ([FromServices] DataBaseService db, CreateBlogDTO blog) =>
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
            app.MapPut("/blog/{id}", async ([FromServices] DataBaseService db, int id, UpdateBlogDTO blog) =>
            {
                var existingBlog = await db.GetBlogById(id);
    
                if (existingBlog is null)
                {
                    return Results.NotFound();
                }

                existingBlog.Titel = blog.Titel;
                existingBlog.Bericht = blog.Bericht;
                existingBlog.Datum = blog.Datum;

                await db.UpdateBlogPost(id, existingBlog);

                return Results.Ok(existingBlog);

            });

            // DELETE Verwijder 1 blog
            app.MapDelete("/blog/{id}", async ([FromServices] DataBaseService db, int id) =>
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
