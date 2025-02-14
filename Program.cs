using EmployeeManagement.Data;
using EmployeeManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // register database context
            builder.Services.AddDbContext<AppDbContext>(
                    options => options.UseInMemoryDatabase("EmployeeDb"));

            //configure CORS policy for angular
            builder.Services.AddCors(
                options =>
                {
                    options.AddPolicy("CORSPolicy", builder => 
                    {
                        builder.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                    });
                }
            );

            // add & configure repository to the dependency injection
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // configure services to add controllers
            builder.Services.AddControllers();

           // add & configure API endpoints
            builder.Services.AddEndpointsApiExplorer();
            
            // add & configure swagger
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // to check if app environment is in development only
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            //specify the kind of CORS policy to use
            app.UseCors("CORSPolicy");

            // call the endpoints for controllers and actions
            app.MapControllers();

            app.Run();
        }
    }
}
