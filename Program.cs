using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TestTask.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configure db
builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Brands}/{action=Index}/{id?}");


//Uncomment to add test data
/*using (var scope = app.Services.CreateScope())
    TestData.Seed(scope.ServiceProvider.GetRequiredService<DataContext>());*/

app.Run();
