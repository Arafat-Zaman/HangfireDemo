using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Step 1: Add Hangfire services and configure in-memory job storage
builder.Services.AddHangfire(config => config
    .UseMemoryStorage()); // Use In-Memory storage instead of SQL Server

builder.Services.AddHangfireServer(); // Start the Hangfire server.

var app = builder.Build();

// Step 2: Enable Hangfire Dashboard
app.UseHangfireDashboard();

// Step 3: Set up a simple recurring job
RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring Job: Hello, Hangfire!"), Cron.Minutely);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Step 4: Map the Hangfire Dashboard to the route '/hangfire'
app.MapHangfireDashboard("/hangfire");

app.Run();
