using AutoMapper;
using Logic.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Agro.ErrorDescriber;
using Agro.Initializer;
using Agro.Localization;
using Agro.Model.Data;
using Agro.Model.Entities;
using Agro.Model.Interfaces;
using Agro.Model.Repository;
using Agro.Model.UnitOfWork;
using AutMapperConfiguration = Logic.MapperConfiguration;

namespace Agro
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                //.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddIdentity<ApplicationUser, IdentityRole>() //(options => options.SignIn.RequireConfirmedAccount = true) 
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<RussianIdentityErrorDescriber>()                
                .AddDefaultTokenProviders()
                .AddDefaultUI()
            ;

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));

            services.AddScoped<RequestService>();

            services.AddSingleton<LocService>();
            
            services.AddAutoMapper(typeof(AutMapperConfiguration.CityProfile));
            //Logic.PaginatedList
            
            

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture("ru-Ru");
                options.AddSupportedUICultures("ru-Ru", "en-US", "de-DE", "ja-JP");
                options.FallBackToParentUICultures = true;

                //options
                //    .RequestCultureProviders.Remove(AcceptLanguageHeaderRequestCultureProvider);

                
                

            });
            
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation().AddViewLocalization();
            //services.AddMvc()
            services.AddMvc(o =>
            {
                o.AllowEmptyInputInBodyModelBinding = true;
            })
            .AddDataAnnotationsLocalization(options => {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                factory.Create(typeof(SharedResource));
            });

            
        }



    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();                
                DbInitializer.Initialize(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Requests}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            

            //app.UseRequestLocalization();

        }
    }
}
