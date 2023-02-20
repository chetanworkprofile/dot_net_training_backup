// Import the namespace containing the ContactsAPIDbContext class
using ContactsApi.Data;
using ContactsApi.Services;

// Import the Entity Framework Core namespace
using Microsoft.EntityFrameworkCore;

// Create a new instance of WebApplication and pass in the args parameter
var builder = WebApplication.CreateBuilder(args);

// Add the Controllers services to the container
builder.Services.AddControllers();

// Add the EndpointsApiExplorer services to the container
builder.Services.AddEndpointsApiExplorer();

// Add the SwaggerGen services to the container
builder.Services.AddSwaggerGen();

// Add the ContactsAPIDbContext to the container
builder.Services.AddDbContext<ContactsAPIDbContext>(options =>
    // Use SQL Server as the database provider
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiConnectionString")));

builder.Services.AddScoped<IService, Service>();

// Build the WebApplication instance
var app = builder.Build();

// If the environment is development, use Swagger and SwaggerUI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use HTTPS redirection
app.UseHttpsRedirection();

// Use authentication and authorization
app.UseAuthorization();

// Map the controllers
app.MapControllers();

// Run the application
app.Run();
