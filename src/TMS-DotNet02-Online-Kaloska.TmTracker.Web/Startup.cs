using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Contexts;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Interfaces;
using TMS_DotNet02_Online_Kaloska.TmTracker.Logic.Managers;
using SignalRApp;
using TMS_DotNet02_Online_Kaloska.TmTracker.Web.Models;
using TMS_DotNet02_Online_Kaloska.TmTracker.Web.Services.MessageService;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IServiceProvider _serviceProvider;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Database context 
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnection")));
            services.AddDbContext<ApplicationContextSingalR>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnectionaSingalR")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddControllersWithViews();
            services.AddSignalR();

            // Custom managers & services
            services.AddScoped(typeof(IRepositoryManager<>), typeof(RepositoryManager<>));
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IProjectManager, ProjectManager>();
            services.AddScoped<IRecordManager, RecordManager>();
            services.AddScoped<IGoalManager, GoalManager>();
            _serviceProvider = services.BuildServiceProvider();//TODO

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/SignalRApp");
            });
        }
    }
}
