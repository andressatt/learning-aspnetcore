using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Lab.Contas.Areas.Identity;
using Lab.Contas.Data;

namespace Lab.Contas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<LabContasContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LabContasContext") ?? throw new InvalidOperationException("Connection string 'LabContasContext' not found.")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                // localize identity error messages
                .AddErrorDescriber<IdentityPortugueseMessages>()
                .AddEntityFrameworkStores<LabContasContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}