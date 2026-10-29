var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidation();
builder.Services.AddDbContext<WPFWOPDRACHT3.Data.PortofolioContext>();

var app = builder.Build();

app.UseSwagger();

app.MapGet("/", () => "");

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "api");
    options.RoutePrefix = string.Empty;
});

app.Run();