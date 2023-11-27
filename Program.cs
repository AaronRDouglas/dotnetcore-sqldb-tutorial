using DotNetCoreSqlDb.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddAzureWebAppDiagnostics();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyDatabaseContext>(options =>
    options.useSqlServer(builder.configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));
builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.configuration = builder.configuration["AZURE_REDIS_CONNECTIONSTRING"];
        options.instancename = "SampleInstance";
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
