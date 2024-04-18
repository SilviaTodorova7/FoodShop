using FoodShop.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FoodShop.Data
{
    public class FoodShopDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public FoodShopDbContext(DbContextOptions<FoodShopDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Comment> Comments { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<TradeMark> TradeMarks { get; set; } = null!;

        public DbSet<ProductType> ProductTypes { get; set; } = null!;

        public DbSet<UserProduct> UserProducts { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserProduct>()
                   .HasKey(t => new { t.UserId, t.ProductId });
            builder.Entity<UserProduct>()
                   .Property(up => up.Date)
                   .HasDefaultValue(DateTime.UtcNow.Date);


            Assembly configAssembly = Assembly.GetAssembly(typeof(FoodShopDbContext)) ?? 
                Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);  
        }
    }
}
