using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Employee;
using DataLayer.User;
using DB.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MVCCoreWithEFCore.ExtensionMethods;
using ServiceLayer.Employee;
using ServiceLayer.UnitOfWork;

namespace MVC_CoreWithEF_Core
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

            services.AddDbContext<MVCEFCoreContext>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
            //services.AddScoped<IEmployeeRepo, EmployeeRepo>();

            services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(30));

            services.AddScoped<ClientIpCheckActionFilter>(container =>
            {
                var loggerFactory = container.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<ClientIpCheckActionFilter>();

                return new ClientIpCheckActionFilter(
                    Configuration["AdminSafeList"], logger);
            });

            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new
                    SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes
                    (Configuration["Jwt:Key"]))
                };
            });

            //          opts =>
            //opts.UseSqlServer(Configuration["DefaultConnection:MVCEFCoreContext"]));
            services.AddMvc();
            //services.AddDbContext<MVCEFCoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MVCEFCoreContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MVCEFCoreContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            db.Database.EnsureCreated();

            app.UseStaticFiles();
            app.UseSession();
            //app.Use(async (context, next) =>
            //{
            //    context.Session.SetObject("CurrentUser",
            //        new { Username = "James", Email = "james@bond.com" });
            //    await next();
            //});
            app.UseMiddleware<AdminSafeListMiddleware>(Configuration["AdminSafeList"]);

            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
