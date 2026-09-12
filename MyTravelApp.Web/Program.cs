using MyTravelApp.Data;
using MyTravelApp.IServices;
using MyTravelApp.Models.Models;
using MyTravelApp.Services;

namespace MyTravelApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connString = builder.Configuration.GetConnectionString("MyConnection");

            builder.Services.AddScoped<DAL>(provider => new DAL(connString));

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<LoginRepository>();

            // Configure RapidAPI options for Booking.com API
            builder.Services.Configure<RapidApiOptions>(
                builder.Configuration.GetSection("RapidApiOptions"));

            // Register HttpClients
            builder.Services.AddHttpClient<WeatherSearchService>();
            builder.Services.AddHttpClient<IBookingComService, BookingComService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}"
            )
            .WithStaticAssets();

            app.Run();
        }
    }
}