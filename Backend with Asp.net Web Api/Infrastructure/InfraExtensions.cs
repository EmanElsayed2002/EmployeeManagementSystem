using Infrastructure.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfraExtensions
    {
        public static IServiceCollection addInfraExtension(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            return services;
        }
    }
}
