using Clearly.Crud.Services;
using Clearly.Crud.WebUi.Client;
using Clearly.Crud.WebUi.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IEntityApiService, EntityApiService>();
builder.Services.AddScoped<HostedCrudApiConfiguration>(sp => new HostedCrudApiConfiguration { BaseUrl = builder.HostEnvironment.BaseAddress });

await builder.Build().RunAsync();
