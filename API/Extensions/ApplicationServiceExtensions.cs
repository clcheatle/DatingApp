using BusinessLogic;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = config.GetConnectionString("DAConnection");

            services.AddDbContext<DataContext>(options => {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IUserRepoLayer, UserRepoLayer>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            return services;
        }
    }
}