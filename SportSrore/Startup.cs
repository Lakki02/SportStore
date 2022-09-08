using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportSrore.Models;
using SportStore.Models;

namespace SportStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        
        
        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            //Необходимо провести миграцию базы данных после этого 
            //add-migration added_{Имя таблицы == имени DbSet в классе ApplicationDbConntext}
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMvc(); //Добавляем сервисы MVC 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting(); //Использум систему шаршрутизации 



            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "pagination",
                    "Products/Page{productPage}",
                    new {Controller = "Product", action = "List"});
                
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=List}/{id?}");
               
            });


            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            // добавление компонентов mvc и определение маршрута
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

           // app.UseMvcWithDefaultRoute();
            /*app.UseMvc(routes =>
            {

            });*/
            SeedData.EnsurePopulated(app);
        }
    }
}
