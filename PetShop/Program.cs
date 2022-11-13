using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Models;
using PetShop.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IRepository<Animal>, AnimalRepository>();
builder.Services.AddDbContext<PetContext>(options => options.UseSqlServer(builder!.Configuration.GetConnectionString("Server")));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PetContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Oops! It seems that the page you are looking for does not exist!");
});

app.Run();
