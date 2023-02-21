using StudentManagementSystemAPI.Services;
using StudentManagementSystemAPI.Modals;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.AddScoped<IService, Service>();*/

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
