using System;

namespace WPFWOPDRACHT3.Endpoints
{
    public static class projectEndpoints
    {
        public static void MapProjectEndpoints(this WebApplication app)
        {

            // GET alle projecten
            app.MapGet("/project", () => );

            // GET 1 project via id
            app.MapGet("/project/{id}", () => );

            // POST 1 project aanmaken
            app.MapPost("/project", () => );

            // PUT Wijzig 1 project
            app.MapPut("/project/{id}", () => );

            // DELETE Verwijder 1 project
            app.MapDelete("/project/{id}", () =>);
        }
    }
}
