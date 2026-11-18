using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WPFWOPDRACHT3.Dtos.AuthDTOs;
using WPFWOPDRACHT3.Models;

namespace WPFWOPDRACHT3.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints (this WebApplication app)
        {
            var authGroup = app.MapGroup("/auth").WithTags("Auth");

            authGroup.MapPost("/register", async ([FromServices] UserManager<UserModel> userManager, RegisterDto dto) => 
            {
                // mappen van dto naar model
                var user = new UserModel 
                { 
                    UserName = dto.Username, 
                    Email = dto.Email 
                };

                // proberen gebruiken te registreren
                var result = await userManager.CreateAsync(user, dto.Password);


                // response
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Gast");
                    
                    return Results.Ok(new { Message = "Gebruiker succesvol geregistreerd als Gast!" });
                }

                return Results.BadRequest(result.Errors);
            });

            authGroup.MapPost("/login", async (LoginDto dto, [FromServices] SignInManager<UserModel> signInManager) =>
            {

                // checken of inloggegevens kloppen
                var result = await signInManager.PasswordSignInAsync(
                    userName: dto.Username, 
                    password: dto.Password, 
                    isPersistent: false, 
                    lockoutOnFailure: false
                );

                // response
                if (result.Succeeded)
                {
                    return Results.Ok(new { Message = "Succesvol ingelogd!" });
                }

                return Results.Unauthorized();
            });
        }   
    }
}
