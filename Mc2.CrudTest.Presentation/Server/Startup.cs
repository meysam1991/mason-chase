using Mc2.CrudTest.Infrastructure.DataBase.Common;
using Mc2.CrudTest.ModelFramework.Translations;
using Mc2.CrudTest.Shared.ErrorMessages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
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

            services.AddTransient<ITranslator, Translator>();
            services.AddScoped<DomainErrorMessages>();
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Shared.Configuration.Mc2Mc2CrudTestFrameworkConfiguration configuration)
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });

        }
    }
}
