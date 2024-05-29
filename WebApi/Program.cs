using Serilog;
using Microsoft.Extensions.Options;
using MaterialSupplyManagement.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var dbSettingsSection = builder.Configuration.GetSection("DBSettings");
builder.Services.Configure<DBSettings>(dbSettingsSection);

builder.Services.AddSingleton(provider =>
{
    var settings = provider.GetRequiredService<IOptions<DBSettings>>().Value;
    var logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    return new MaterialSupplyRepository(settings, logger);
});

var app = builder.Build();

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
	pattern: "{controller=MaterialSupply}/{action=Index}/{id?}");

app.Run();