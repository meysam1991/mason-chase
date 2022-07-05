using Mc2.CrudTest.DomainModel.Customer.Data;
using Mc2.CrudTest.Infrastructure.DataBase.Common;
using Mc2.CrudTest.Infrastructure.DataBase.Customer;
using Mc2.CrudTest.ModelFramework.Command;
using Mc2.CrudTest.ModelFramework.Translations;
using Mc2.CrudTest.Shared.ErrorMessages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MC2.API
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MC2.API", Version = "v1" });
            });

            services.AddTransient<ICustomerCommandRepository, CustomerCommandRepository>();
            services.AddTransient<ITranslator, Translator>();
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<DomainErrorMessages>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MC2.API v1"));
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
