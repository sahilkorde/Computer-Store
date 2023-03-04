using Blazored.LocalStorage;
using Computer_Store_Client;
using Computer_Store_Client.Service;
using Computer_Store_Client.Service.IService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartSerivce>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
