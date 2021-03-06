using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.IO;
using System;
using WebAPI;

using AutoMapper;

using EFTasks.DAL;
using EFTasks.BLL;
using EFTasks.BLL.Abstractions;
using EFTasks.BLL.Services;
using EFTasks.DAL.Abstractions;

namespace EFTasks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        { 
            var psqlConnection = Configuration.GetConnectionString("PostgreSQLConnectionString");
            services.AddDbContextPool<Context>(options => options.UseNpgsql(psqlConnection));

            services.AddAutoMapper(typeof(ConfigurationMapper));
            services.AddControllers();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITaskService, CustomTaskService>();

            if (!int.TryParse(Configuration["ActualizePeriodInMinutes"], out int actualizePeriod))
               actualizePeriod = 2;

            services.AddHostedService(
                p => new TaskHostedService(p.GetService<ITaskService>(), actualizePeriod));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Task  API",
                        Version = "v1"
                    });
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                c.IncludeXmlComments(filePath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                c.RoutePrefix = "";
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
