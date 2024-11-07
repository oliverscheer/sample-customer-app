using Customer.Web.Client.APIs;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder
    .Services
        .AddScoped(sp => new HttpClient { 
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
        });

builder
    .Services
        .AddRefitClient<ICountryService>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri("https+http://customerapi"));


builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();
