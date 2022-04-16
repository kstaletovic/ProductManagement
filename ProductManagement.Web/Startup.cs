using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Interfaces.Repositories;
using ProductManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Web
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
            services.AddDbContext<ProductManagementContext>(options =>
            options.UseLazyLoadingProxies()
                .UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            if (Configuration["DataSource"].Equals("db"))
            {
                services.AddScoped<IProductRepository, ProductRepositoryDb>();
                services.AddScoped<ICategoryRepository, CategoryRepositoryDb>();
                services.AddScoped<IManufacturerRepository, ManufacturerRepositoryDb>();
                services.AddScoped<ISupplierRepository, SupplierRepositoryDb>();
            }
            else
            {
                services.AddScoped<IProductRepository, ProductRepositoryJson>();
                services.AddScoped<ICategoryRepository, CategoryRepositoryJson>();
                services.AddScoped<IManufacturerRepository, ManufacturerRepositoryJson>();
                services.AddScoped<ISupplierRepository, SupplierRepositoryJson>();
            }

            services.AddScoped<IProductService, ProductService>();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });
        }
    }
}
