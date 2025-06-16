using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


// Selects endpoints to use Routings
app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "adminAr",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "paging_and_sort",
    pattern: "{controller}/{action}/{id}/page{num}/sort-by-{sortBy}");

app.MapControllerRoute(
    name: "paging",
    pattern: "{controller}/{action}/{id}/page{num}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{area}/{controller=Home}/{action=countDown}/{id}");

app.Run();
