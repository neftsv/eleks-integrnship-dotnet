using InternetShop.Data;
using InternetShop.Interface;
using InternetShop.Models;
using InternetShop.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Work in the database is related to user registration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("InternetShopCRMcnn")));

// Add authentication services

builder.Services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	})
	.AddCookie(options =>
	{
		options.Cookie.Name = "YourAppCookieName";
	});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    Seed.SeedData(app);
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
