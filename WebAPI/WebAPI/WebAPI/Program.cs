using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Models;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ItemsDB>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDb")));

            builder.Services.AddCors((corsoptions) =>
            {
                corsoptions.AddPolicy("Mypolicy", (policyoptions) =>
                {
                    policyoptions.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("Mypolicy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}