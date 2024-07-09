using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concreate;
using StoreAppWeb.Models;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<StoreDbContext>(opt=>opt.UseMySql(builder.Configuration.GetConnectionString("mysql_connection"),ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql_connection")),b=>b.MigrationsAssembly(nameof(StoreAppWeb))));
builder.Services.AddDbContext<StoreDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("mssql_connection"),b=>b.MigrationsAssembly("StoreAppWeb")));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped(typeof(IStoreRepository<>),typeof(EfStoreRepository<>));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
//bize httpcontexi verir
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<Cart>(sc => SessionCart.GetCart(sc));
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();
var app = builder.Build();


app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute("category_filter", "/urunler/{category}", new{controller="Home",Action="Index"});
app.MapControllerRoute("urunswithpages","/urunler/{page}",new{controller="Home",Action="Index"});
app.MapControllerRoute("products_details", "/{name}", new { controller = "Home", Action = "Index" });

app.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
