using ChatApp;
using ChatApp.Controllers;
using ChatApp.Models.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChatDBContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ChatSystemWebContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var defaultCulture = new CultureInfo("en-US");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	options.DefaultRequestCulture = new RequestCulture(defaultCulture);
	options.SupportedCultures = new List<CultureInfo> { defaultCulture };
	options.SupportedUICultures = new List<CultureInfo> { defaultCulture };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	SampleData.Initialize(services);
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseRequestLocalization("en-US");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHub<MessangerHub>("/Messanger");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
