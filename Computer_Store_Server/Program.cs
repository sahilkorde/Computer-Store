using Computer_Store_Server.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Computer_Store_DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Computer_Store_Business.Repository.IRepository;
using Computer_Store_Business.Repository;
using Computer_Store_Server.Service;
using Computer_Store_Server.Service.IService;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTE0ODY5NEAzMjMwMmUzNDJlMzBVam8zd21wcFN1NlBpalNzM2xPb0VJOEdJdlU2bTd2dWg5cWlGQ3FsYm4wPQ==");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();;

app.Run();
