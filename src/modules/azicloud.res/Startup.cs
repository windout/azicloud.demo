using azicloud.res.Application.Interfaces;
using azicloud.res.Application.Responsitory;
using azicloud.res.bo;
using azicloud.res.GrpcServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace azicloud.res
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "azicloud.res", Version = "v1" });
            });           
            services.AddResService(Configuration.GetConnectionString("db_task"));
            services.AddTransient<IWikiCategoryService, WikiCategoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();              
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "azicloud.res v1"));
            }
            app.UseHttpsRedirection();

            app.UseRouting();       

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<WikiCategoryGrpcService>();
                endpoints.MapControllers();
            });
        }
    }
}
