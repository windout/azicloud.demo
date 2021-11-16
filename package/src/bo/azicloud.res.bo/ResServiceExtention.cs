using Microsoft.Extensions.DependencyInjection;
using azicloud.res.bo.Interfaces;
using azicloud.res.bo.DbContext;
using azicloud.res.bo.category;

namespace azicloud.res.bo
{
    public static class ResServiceExtention
    {
        public static IServiceCollection AddResService(this IServiceCollection services, string conStr)
        {
            services.AddScoped<IDapperService>(sp=>new DapperService(conStr));
            services.AddScoped<Iwiki_category,wiki_category>();
            return services;
        }
    }
}
