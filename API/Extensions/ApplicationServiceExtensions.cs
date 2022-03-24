using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using API.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {   
            //sledzenie w czasie rzeczywistym czy uzytkownik jest online czy nie 
            services.AddSingleton<PresenceTracker>();
            //chmura do zdjec
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            //zakres dzialania tokenu
            services.AddScoped<ITokenService, TokenService>();
            //dodanie serwisu dla dodawania i usuwania zdjec
            services.AddScoped<IPhotoService, PhotoService>();
            //dodanie serwisu dla polubień
            services.AddScoped<ILikesRepository, LikesRepository>();
            //dodanie serwisu dla wiadomosci
            services.AddScoped<IMessageRepository, MessageRepository>();
            //dodanie serwisu dla "kiedy ostatnio uzytkownik byl aktywny"
            services.AddScoped<LogUserActivity>();
            //dodanie serwisu dla uzytkownika
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