using FoodShop.Data;
using FoodShop.Data.Models;
using FoodShop.Services;
using FoodShop.Services.Interfaces;
using FoodShop.Web.Infrastructure.Extensions;
using FoodShop.Web.Infrastructure.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static FoodShop.Common.GeneralApplicationConstants;

namespace FoodShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<FoodShopDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = builder.Configuration
                .GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.Password.RequireDigit = builder.Configuration
                .GetValue<bool>("Identity:Password:RequireDigit");
                options.Password.RequireLowercase = builder.Configuration
                .GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireUppercase = builder.Configuration
                .GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequireNonAlphanumeric = builder.Configuration
                .GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequiredLength = builder.Configuration
                .GetValue<int>("Identity:Password:RequiredLength");
            })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<FoodShopDbContext>();

            builder.Services
                .AddControllersWithViews()
                .AddMvcOptions(options =>
                {
                    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                });

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ITradeMarkService, TradeMarkService>();
            builder.Services.AddScoped<ICommentService, CommentService>();

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.SeedAdministrator(DevelopmentAdminEmail);

            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();
        }
    }
}