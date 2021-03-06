using System;
using Arch.EntityFrameworkCore.UnitOfWork;
using DAL.Contexts;
using DAL.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;

namespace NetCoreMultilayeredArchitecture
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Config.Instanse(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), AppDomain.CurrentDomain.GetAssemblies())
                .AddDbContext<DbEntityContext>(options => options.UseNpgsql(Config.Get<string>("ConnectionStringDBEF")))
                .AddUnitOfWork<DbEntityContext>()
                .AddScoped<IAccountService, AccountService>()
                .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
