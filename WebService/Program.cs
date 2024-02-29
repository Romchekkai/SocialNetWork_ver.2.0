using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialNetWork.BL;
using SocialNetWork.BL.Extentions;
using SocialNetWork.DAL;
using SocialNetWork.DAL.Models.Users;
using SocialNetWork.DAL.Repository;


namespace SocialNetWork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add DB Connection
            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));


            //Add DB Mapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services
                .AddUnitOfWork()
                    .AddCustomRepository<Message, MessageRepository>().AddCustomRepository<Friend, FriendsRepository>()
                .AddIdentity<User, IdentityRole>(opts => {
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                    .AddEntityFrameworkStores<ApplicationDbContext>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
