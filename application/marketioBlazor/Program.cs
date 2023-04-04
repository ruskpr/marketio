using Blazored.SessionStorage;
using Blazored.Toast;
using Common;
using marketioBlazor.Authentication;
using marketioBlazor.Data;
using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton(new RestClient(new HttpClient())); // added
builder.Services.AddSingleton<ApiClientService>(); // added
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>(); // added
builder.Services.AddBlazoredSessionStorage(); // added
builder.Services.AddAuthenticationCore();
builder.Services.AddAuthentication("Identity.Application").AddCookie();
builder.Services.AddBlazoredToast(); // added

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
