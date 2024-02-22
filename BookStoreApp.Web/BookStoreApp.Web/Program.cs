using Blazored.LocalStorage;
using BookStoreApp.Web.Configurations;
using BookStoreApp.Shared.Providers;
using BookStoreApp.Shared.Services.Authentication;
using BookStoreApp.Shared.Services.Authors;
using BookStoreApp.Web.Components;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<CookiesEvents>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.EventsType = typeof(CookiesEvents);
});

builder.Services.AddHttpClient();

builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BookStoreApp.Web.Client._Imports).Assembly);

await app.RunAsync();
