using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Smart.Core.Entities;
using Smart.Infrastructure.Data;
using Smart.WebApi.ServiceExtension;
using Smart.Core;
using Smart.Infrastructure;
using Smart.Core.Helpers;
using ServiceStack;
using System.Configuration;
namespace Smart
{
    public class Program
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {


            services.AddControllers();
            services.AddDbContext(configuration.GetConnectionString("DefaultConnection"));
            services.AddIdentityDbContext();
            services.AddAuthentication();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            services.AddRepositories();
            services.AddCustomServices();
            services.AddFluentValitation();
            services.AddSwagger();
            services.ConfigureImageSettings(configuration);
            services.AddAutoMapper();
            services.AddJwtAuthentication(configuration);
            services.AddMvcCore().AddRazorViewEngine();

            services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            ConfigureServices(builder.Services, builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddSwaggerGen(options => { });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(options => { });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}