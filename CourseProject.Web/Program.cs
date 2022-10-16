using CourseProject.DataContext;
using CourseProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using CourseProject.Application.Service;
using CourseProject.DataContext.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using CourseProject.Application.CQRS.Commands;

var webApplication = WebApplication.CreateBuilder(args);

// add identitybuilder and configure password settings
IdentityBuilder builder = webApplication.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequiredLength = 4;
});

// add identity settings
builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
builder.AddEntityFrameworkStores<ApplicationDbContext>();
builder.AddRoleManager<RoleManager<IdentityRole>>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// add database
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(webApplication.Configuration.GetConnectionString("DefaultConnection")));

// add DI
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();

// add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// add MediatR
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

// add Auth
builder.Services.AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme);

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = webApplication.Build();

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

app.Run();
