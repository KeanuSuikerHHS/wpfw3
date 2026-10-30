using System;
using Microsoft.AspNetCore.Mvc;
using WPFWOPDRACHT3.Dtos;
using WPFWOPDRACHT3.Dtos.ProjectDTOs;
using WPFWOPDRACHT3.Models;
using WPFWOPDRACHT3.Services;

namespace WPFWOPDRACHT3.Endpoints
{
    public static class projectEndpoints
    {
        public static void MapProjectEndpoints(this WebApplication app)
        {

            // GET alle projecten
            app.MapGet("/project", async([FromServices] DataBaseService db) =>
            {
                var projecten = await db.GetAllProjecten();
                return projecten is not null ? Results.Ok(projecten) : Results.NoContent();
            });

            // GET 1 project via id
            app.MapGet("/project/{id}", async ([FromServices] DataBaseService db, int id) =>
            {
                var project = await db.GetProjectById(id);
                return project is not null ? Results.Ok(project) : Results.NotFound();
            });

            // POST 1 project aanmaken
            app.MapPost("/project", async ([FromServices] DataBaseService db, CreateProjectDTO project) =>
            {
                ProjectModel projectModel = new ProjectModel
                {
                    Titel = project.Titel,
                    Beschrijving = project.Beschrijving,
                    Categorie = project.Categorie,
                    GitHubURL = project.GitHubURL,
                    Datum = project.Datum,
                };

                await db.AddProject(projectModel);
                return Results.Created($"/project/{projectModel.Id}", projectModel);
            });

            // PUT Wijzig 1 project
            app.MapPut("/project/{id}", async ([FromServices] DataBaseService db, int id, UpdateProjectDTO project) =>
            {
                var existingProject = await db.GetProjectById(id);
    
                if (existingProject is null)
                {
                    return Results.NotFound();
                }

                existingProject.Titel = project.Titel;
                existingProject.Beschrijving = project.Beschrijving;
                existingProject.Categorie = project.Categorie;
                existingProject.GitHubURL = project.GitHubURL;
                existingProject.Datum = project.Datum;

                await db.UpdateProject(id, existingProject);

                return Results.Ok(existingProject);

            });

            // DELETE Verwijder 1 project
            app.MapDelete("/project/{id}", async ([FromServices] DataBaseService db, int id) =>
            {
                var existingProject = await db.GetProjectById(id);
    
                if (existingProject is null)
                {
                    return Results.NotFound();
                }


                await db.DeleteProject(id);

                return Results.NoContent();
            });
        }
    }
}
