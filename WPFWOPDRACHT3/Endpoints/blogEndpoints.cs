using System;

namespace WPFWOPDRACHT3.Endpoints
{
    public static class blogEndpoints
    {
        public static void MapBlogEndpoints(this WebApplication app)
        {

            // GET alle projecten
            app.MapGet("/blog", () => );

            // GET 1 project via id
            app.MapGet("/blog/{id}", () => );

            // POST 1 project aanmaken
            app.MapPost("/blog", () => );

            // PUT Wijzig 1 project
            app.MapPut("/blog/{id}", () => );

            // DELETE Verwijder 1 project
            app.MapDelete("/blog/{id}", () =>);
        }
    }
}
