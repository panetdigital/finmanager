using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AppFinancia;
using Blazored.LocalStorage;
using AppFinancia.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

// Registra o serviço LocalStorageService com  javascript
builder.Services.AddScoped<LocalStorageService>();

builder.Services.AddScoped<TransactionService>();
// Adicionando o CsvExportService ao contêiner de injeção de dependências
builder.Services.AddScoped<CsvExportService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
