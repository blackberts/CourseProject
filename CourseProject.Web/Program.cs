using CourseProject.DataContext;
using CourseProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using CourseProject.Application.Service;
using CourseProject.DataContext.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using CourseProject.Application.UoW;
using CourseProject.Application.CQRS.Commands.Tags;
using System.Reflection;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

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
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddDataAnnotationsLocalization()
    .AddViewLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("ru")
    };

    options.DefaultRequestCulture = new RequestCulture("ru");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
builder.Services.AddControllersWithViews();

// add database
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(webApplication.Configuration.GetConnectionString("DefaultConnection")));

// add DI
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IItemsRepository, ItemsRepository>();
builder.Services.AddTransient<ITagsRepository, TagsRepository>();
builder.Services.AddTransient<ICollectionsRepository, CollectionsRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

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

app.UseRequestLocalization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
