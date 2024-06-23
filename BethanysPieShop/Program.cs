using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BethanysPieShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IPieRepository, PieRepository>();
            builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            builder.Services.AddSession();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            
            builder.Services.AddDbContext<BethanysPieShopDbContext>(options => { options.UseNpgsql(builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]); });
            
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseSession();
            if(app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.MapDefaultControllerRoute();
            DbInitializer.Seed(app);
            app.Run();
        }
    }
}
