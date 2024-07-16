using ConsoleApp1.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\05._ASP.NET\Silicon_Website\Infrastructure\Data\silicon_db.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

    services.AddScoped<AddressRepository>();
    //services.AddScoped<CourseAuthorRepository>();
    //services.AddScoped<CourseRepository>();
    services.AddScoped<ProfilePictureRepository>();
    services.AddScoped<UserProfileRepository>();
    //services.AddScoped<UserSavedItemRepository>();
    services.AddScoped<MenuService>();

}).Build();
builder.Start();

var menuService = builder.Services.GetRequiredService<MenuService>();

await menuService.Run();


