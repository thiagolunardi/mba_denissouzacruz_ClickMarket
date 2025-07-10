using ClickMarket.AppMvc.Configurations;
using ClickMarket.AppMvc.Data;
using ClickMarket.Business.Interfaces;
using ClickMarket.Data.Context;
using ClickMarket.Data.Repository;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddDatabaseSelector()
    .AddMvcConfig()
    .RegisterServices()
    .AddIdentityConfig();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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
app.MapRazorPages();

app.UseDbMigrationHelper();

app.Run();
