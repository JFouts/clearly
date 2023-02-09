using Clearly.Crud.Services;
using Clearly.Crud.WebUi;
using Clearly.Crud.WebUi.Client;
using Clearly.Crud.WebUi.Core.Services;
using Clearly.Crud.WebUi.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IEntityApiService, EntityApiService>();
builder.Services.AddScoped<IEntityDefinitionApiService, EntityDefinitionApiService>();

// TODO: Use IOptions?
builder.Services.AddScoped(sp => new HostedCrudApiConfiguration { BaseUrl = builder.HostEnvironment.BaseAddress });

builder.Services.AddScoped<IDataSourceFactory, DataSourceFactory>();
builder.Services.AddScoped(typeof(IDataSourceReader<>), typeof(AutoMapperDataSourceReader<>));
builder.Services.AddAutoMapper(typeof(App).Assembly); // TODO: Figure out how we want to define automapper profiles, or if there is a better alternative

await builder.Build().RunAsync();
