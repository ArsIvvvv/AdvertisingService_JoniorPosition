
using AdvertisingService.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace AdvertisingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAdvertisingServices();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Advertising Service API",
                    Version = "v1",
                    Description = "API для управления рекламными площадками и локациями",
                });
            });

                builder.Services.AddControllers();
           
            builder.Services.AddOpenApi();

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
