using Blazored.SessionStorage;
using Common;
using marketioBlazor.Authentication;
using marketioBlazor.Data;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredSessionStorage(); // added
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>(); // added
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<ApiClientService>(); // added
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}



app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
