using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WPFWOPDRACHT3.Data;
using WPFWOPDRACHT3.Endpoints;
using WPFWOPDRACHT3.Models;
using WPFWOPDRACHT3.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidation();
builder.Services.AddScoped<DataBaseService>();
builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<PortofolioContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EditorOrAdmin", policy =>
        policy.RequireRole("Editor", "Admin"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PortofolioContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    
    string[] roleNames = { "Gast", "Editor", "Admin" };
    
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

app.UseAuthentication();
app.UseAuthorization();
app.MapBlogEndpoints();
app.MapProjectEndpoints();
app.MapAuthEndpoints();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "api");
    options.RoutePrefix = string.Empty;
});

app.Run();