using Infrastructure.Contexts;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using WebApp.Services;

var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\SiliconWebsite\Infrastructure\Data\silicon_dbase.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedEmail = false;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
    x.Password.RequireUppercase = true;
}).AddEntityFrameworkStores<DataContext>();


builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<ProfilePictureRepository>();
builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CourseAuthorRepository>();
builder.Services.AddScoped<SavedCoursesRepository>();
builder.Services.AddScoped<ContactRequestRepository>();
builder.Services.AddScoped<CourseCategoryRepository>();

builder.Services.AddScoped<UserProfileService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<WebAppCourseService>();

builder.Services.AddAuthentication().AddFacebook(x => 
{
    x.AppId = "269272056121983";
    x.AppSecret = "624813c34c7cd0e486bedfb27d6f9b29";
});

builder.Services.AddAuthentication().AddGoogle(x =>
{
    x.ClientId = "1047036215054-r8sdc3bphqdgt3aqbmp590c241t48ckd.apps.googleusercontent.com";
    x.ClientSecret = "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";
});

var app = builder.Build();

app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();