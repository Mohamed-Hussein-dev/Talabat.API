using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using Microsoft.OpenApi.Writers;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helper;
using Talabat.APIs.MiddleWares;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnectionString"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdintyDbConnectionString"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var connectionString = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connectionString);
            });

            builder.Services.AddAppServeses(); // extension method
            builder.Services.AddIdentityServeses(builder.Configuration);
            #endregion

            var app = builder.Build();

            #region Update - Database
            using var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var LoggerFacotry = Services.GetRequiredService<ILoggerFactory>();
            var userManager = Services.GetRequiredService<UserManager<AppUser>>();
            try 
            {
                var dbContext = Services.GetRequiredService<StoreDbContext>();
                await dbContext.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(dbContext);

                var IdentityDbContext = Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
            }
            catch (Exception ex) 
            {
                var Logger = LoggerFacotry.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Occaur During Update Database");
            }
           
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithRedirects("/erorr/{0}");
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
