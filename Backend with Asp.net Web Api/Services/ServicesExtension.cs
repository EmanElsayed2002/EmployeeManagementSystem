using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;
using Services.Implementation;
using System.Reflection;

namespace Services
{
    public static class ServicesExtension
    {
        public static IServiceCollection addServiceExtension(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
