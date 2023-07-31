using ApplicationSrv.iRepo;
using ApplicationSrv.Repo;
using DataSrv;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationSrv
{
    public static class ApplicationDI
    {
        public static void ApplicationDepencdecyInjection(this IServiceCollection service)
        {
            service.DataDependencyinjection();
            service.AddScoped<IUserRepository,UserRepository>();
        }
    }
}
