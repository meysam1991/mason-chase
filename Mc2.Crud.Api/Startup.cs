using Mc2.CrudTest.Infrastructure.DataBase.Common;
using Mc2.CrudTest.ModelFramework.Configuration;
using Mc2.CrudTest.ModelFramework.StartupExtensions;
using Mc2.CrudTest.Shared.Serializations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Mc2.Crud.Api
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
            services.AddDbContextPool<Mc2CrudTestDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Mc2CRUD"),
                    conf =>
                    {
                        conf.UseHierarchyId();
                        conf.EnableRetryOnFailure(3);
                    });
            });

            services.AddDbContextPool<EfBaseMc2CrudTest>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Mc2CRUD"),
                    conf =>
                        conf.UseHierarchyId());
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mc2.Crud.Api", Version = "v1" });
            });
            services.AddTransient<IJsonSerializer, NewtonSoftSerializer>();
            services.AddSirvanTspFrameworkServices(Configuration);
            services.AddCors(x =>
            {
                x.AddPolicy("Any", b =>
                {
                    b.AllowAnyOrigin();
                    b.AllowAnyHeader();
                    b.AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Mc2CrudTestFrameworkConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mc2.Crud.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Any");

            app.UseRouting();

            var locale = configuration.SiteLocale;
            var localizationOptions = new RequestLocalizationOptions
            {
                SupportedCultures = new List<CultureInfo> { new(locale) },
                SupportedUICultures = new List<CultureInfo> { new(locale) },
                DefaultRequestCulture = new RequestCulture(locale)
            };

            app.UseRequestLocalization(localizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
