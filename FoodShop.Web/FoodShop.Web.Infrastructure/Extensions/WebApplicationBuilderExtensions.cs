﻿using FoodShop.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static FoodShop.Common.GeneralApplicationConstants;

namespace FoodShop.Web.Infrastructure.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager = 
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            RoleManager<IdentityRole<Guid>> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }
                
                IdentityRole<Guid> role = new IdentityRole<Guid>(AdminRoleName);

                await roleManager.CreateAsync(role);

                ApplicationUser adminUser = await userManager
                    .FindByEmailAsync(email);

                await userManager.AddToRoleAsync(adminUser, AdminRoleName);


            }).GetAwaiter().GetResult();

            return app;
        }
    }
}
