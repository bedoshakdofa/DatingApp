using DatingApp.Data;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using DatingApp.Services;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Extentions
{
    public static class ApplicationServiceExtention
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DbContextApplication>(option => option.UseNpgsql(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRepository, UserRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService,PhotoServices>();
            services.AddScoped<LogActivity>();
            services.AddScoped<ILikeRespository,LikesRepository>();
            return services;
        }
    }
}
