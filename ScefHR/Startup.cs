using System;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScefHR.Helpers;
using ScefHR.Models;
using ScefHR.Services;
using AutoMapper;
using ScefHR.Controllers;

namespace ScefHR
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(options =>
        {    
            options.DefaultAuthenticateScheme = "JwtBearer";
                
            options.DefaultChallengeScheme = "JwtBearer";     
        })
        .AddJwtBearer("JwtBearer", jwtOptions =>
        {    
            jwtOptions.TokenValidationParameters = new TokenValidationParameters()
            {    
                // The SigningKey is defined in the TokenController class
                IssuerSigningKey = TokenController.SIGNING_KEY,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };
        });

            services.AddCors();
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();
            services.AddAutoMapper(typeof(Startup));
            //services.AddDbContext<AppIdentityDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = HRScef; Trusted_Connection = True;"));
            services.AddDbContext<DataContext>(options => options.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = HRScef; Trusted_Connection = True;"));
            
            
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEntityService, EntityService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFormFieldService, FormFieldService>();
            services.AddScoped<IServiceFormService, ServiceFormService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
