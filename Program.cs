using DotNetCoreSqlDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.StackExchangeRedis;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddAzureWebAppDiagnostics();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyDatabaseContext>(options =>
    options.UseSqlServer(("AZURE_SQL_CONNECTIONSTRING")));
builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration["AZURE_REDIS_CONNECTIONSTRING"];
        options.InstanceName = "SampleInstance";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todos}/{action=Index}/{id?}");

app.Run();
