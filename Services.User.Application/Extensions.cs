using Microsoft.Extensions.DependencyInjection;
using Services.User.Application.Services;
using Services.User.Domain.Services;

namespace Services.User.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services
                .AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
