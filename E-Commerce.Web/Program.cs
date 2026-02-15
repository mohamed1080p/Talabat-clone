
using AutoMapper;
using Domain.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using persistence;
using persistence.Data;
using persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using Shared.ErrorModels;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddScoped<PictureUrlResolver>();
            builder.Services.AddWebApplicationServices();
            builder.Services.AddJWTService(builder.Configuration);

            var app = builder.Build();

            await app.SeedDataBaseAsync();

            // Use Custom Exception MiddleWare
            app.UseCustomExceptionMiddleWare();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                   app.UseSwaggerMiddleWares();
                }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
