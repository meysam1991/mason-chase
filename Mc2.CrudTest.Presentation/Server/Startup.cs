using Mc2.CrudTest.Infrastructure.DataBase.Common;
using Mc2.CrudTest.ModelFramework.Configuration;
using Mc2.CrudTest.ModelFramework.StartupExtensions;
using Mc2.CrudTest.ModelFramework.Translations;
using Mc2.CrudTest.Shared.ErrorMessages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Globalization;

namespace Mc2.CrudTest.Presentation.Server
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
            
            services.AddSwaggerGen();
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<ITranslator, Translator>();
            services.AddScoped<DomainErrorMessages>();

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
                app.UseWebAssemblyDebugging();
               
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger/ui";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI(v1)");
            });
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
