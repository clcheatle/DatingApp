using API.BusinessLogic;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Repository;
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
            services.AddSingleton<PresenceTracker>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            string connectionString = config.GetConnectionString("DAConnection");

            services.AddDbContext<DataContext>(options => {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<LogUserActivity>();
            services.AddScoped<MessagesLogic>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            

            return services;
        }
    }
}