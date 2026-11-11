using Microsoft.EntityFrameworkCore;
using WPFWOPDRACHT3.Data;
using WPFWOPDRACHT3.Endpoints;
using WPFWOPDRACHT3.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidation();
builder.Services.AddScoped<DataBaseService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PortofolioContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapBlogEndpoints();
app.MapProjectEndpoints();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "api");
    options.RoutePrefix = string.Empty;
});

app.Run();