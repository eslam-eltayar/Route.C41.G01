using Microsoft.Extensions.DependencyInjection;
using Route.C41.G01.BBL.Interfaces;
using Route.C41.G01.BBL.Repositories;

namespace Route.C41.G01.PL.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();


            return services;
        }
    }
}
