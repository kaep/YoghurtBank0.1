using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using YoghurtBank.Data;
using YoghurtBank.Infrastructure;
using Microsoft.EntityFrameworkCore;
using YoghurtBank.Data.Model;
using Microsoft.EntityFrameworkCore.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

//database connection
builder.Services.AddDbContextFactory<YoghurtContext>(opt => opt.UseNpgsql(@"Host=localhost;Database=entitycore;Username=postgres;Password=mypassword"));
builder.Services.AddScoped<IYoghurtContext, YoghurtContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

var context = app.Services.GetService<IYoghurtContext>();
var usertest = new Student{Id = 1};
context.Users.Add(usertest);
context.SaveChangesAsync();



