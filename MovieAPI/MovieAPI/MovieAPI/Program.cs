using Microsoft.EntityFrameworkCore;
using MovieAPI;
using MovieAPI.MovieDB;
using MovieAPI.Repo;
using MovieAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<SlingContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMovieRepo<Movie>,RepoClass>();
builder.Services.AddTransient<IService<Movie>,ServiceRepo>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(options => options.WithOrigins("http://localhost:4200/").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
app.Run();
