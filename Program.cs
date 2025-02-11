using EmployeeManagement.Data;
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

            var app = builder.Build();

            //specify the kind of CORS policy to use
            app.UseCors("CORSPolicy");

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
