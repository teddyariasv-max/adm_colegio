using adm_colegio_front;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7132/") });
builder.Services.AddScoped<adm_colegio_front.Services.Persona_service>();
builder.Services.AddScoped<adm_colegio_front.Services.Materia_service>();
builder.Services.AddScoped<adm_colegio_front.Services.Matricula_service>();
builder.Services.AddScoped<adm_colegio_front.Services.Asignacion_service>();
builder.Services.AddScoped<adm_colegio_front.Services.Reporte_service>();

await builder.Build().RunAsync();
