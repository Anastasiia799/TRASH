using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TRASH.InfrastructureServices.Gateways.Database;
using Microsoft.EntityFrameworkCore;
using TRASH.ApplicationServices.GetYardAreaListUseCase;
using TRASH.ApplicationServices.Ports.Gateways.Database;
using TRASH.ApplicationServices.Repositories;
using TRASH.DomainObjects.Ports;

namespace TRASH.WebService
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
            services.AddDbContext<TRASHContext>(opts => 
                opts.UseSqlite($"Filename={System.IO.Path.Combine(System.Environment.CurrentDirectory, "TRASH.db")}")
            );

            services.AddScoped<ITRASHDatabaseGateway, TRASHEFSqliteGateway>();

            services.AddScoped<DbTRASHRepository>();
            services.AddScoped<IReadOnlyTRASHRepository>(x => x.GetRequiredService<DbTRASHRepository>());
            services.AddScoped<IRTRASHRepository>(x => x.GetRequiredService<DbTRASHRepository>());


            services.AddScoped<IGetTRASHListUseCase, GetTRASHListUseCase>();

            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}