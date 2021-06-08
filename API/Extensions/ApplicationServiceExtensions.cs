using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {   
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            //zakres dzialania tokenu
            services.AddScoped<ITokenService, TokenService>();
            //dodanie serwisu dla repozytorium
            services.AddScoped<IUserRepository, UserRepository>();
            //dodanie automappera i okreslenie z ktorego profilu ma korzystac
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            //dodanie konfiguracji bazy danych
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            
            return services;
        }
    }
}