using WPFWOPDRACHT3.Endpoints;
using WPFWOPDRACHT3.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidation();
builder.Services.AddDbContext<WPFWOPDRACHT3.Data.PortofolioContext>();
builder.Services.AddScoped<DataBaseService>();

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