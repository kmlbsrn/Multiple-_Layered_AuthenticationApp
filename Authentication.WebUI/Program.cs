using Authentacion.Data.Context;
using Authentacion.Data.Repositories;
using Authentication.Business.Managers;
using Authentication.Business.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("HomeConnection");

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AuthenticationAppContext>(options=> options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IUserRepository),typeof(UserRepository));

builder.Services.AddScoped<IUserService, UserManager>();

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.LogoutPath= new PathString("/");
    options.AccessDeniedPath = new PathString("/");
});



var app = builder.Build();

app.MapDefaultControllerRoute();


app.UseAuthentication();
app.UseAuthorization();

 
app.Run();
