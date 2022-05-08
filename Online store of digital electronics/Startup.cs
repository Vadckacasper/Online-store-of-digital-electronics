using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Online_store_of_digital_electronics.Data;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Online_store_of_digital_electronics.Models;

namespace Online_store_of_digital_electronics
{

    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddControllersWithViews();
            services.AddDbContext<ShopContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<Buyers, IdentityRole>().AddEntityFrameworkStores<ShopContext>();

            //    services.AddIdentityCore<IdentityUser>(
            //options => options.SignIn.RequireConfirmedAccount = true)
            //.AddRoles<IdentityRole>()
            //.AddEntityFrameworkStores<ShopContext>();
            services.AddMvc();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => 
        {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
