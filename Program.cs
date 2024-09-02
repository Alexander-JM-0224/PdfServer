using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using PdfServer;
using PdfServer.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazorise(options =>
{
    options.Debounce = true;
    options.DebounceInterval = 300;
    options.ProductToken = "CjxRBHF7Nww9VwF1fzQ1BlEAc3s1DD1VAHZ6NQg0bjoNJ2ZdYhBVCCo/CTtTPUsNalV8Al44B2ECAWllMit3cWhZPUsCbFtpDUMkGnxIaVliJClwVG0RPUsRWnxNN3EGHEx8Uzx9ABZaZ14sZxIRWgJCLG8NB0hxWDA9SxFaeVk3fwIBSGhAJmQEEVp1TTtvHhxKb188b3sASmdAKn0IGlY1BjxvAgZEalgwbx4DRGBTPGIOGVZnU1l+DhFJcUEqZBJDD2dTL3kSGlNxSTRvHgNEYFM8Yg4ZVmdTWX4OEUlxQSpkEkMPZ1M3YgQLU3FJNG8eA0RgUzxiDhlWZ1NZfg4RSXFBKmQSQw9ufDYACX1ueWItfg8ofUh/NHExKioLSCZ5JQt/Yl9IBHR8f2hZB2gTKFxaWTFjMBR3Xz8WBDI5TXJJF3sMCmN2R1R0dRtiaFQlew4sQkB0AWUTDH0PVFN8Lg8wWVUufWphaXVdAkUWCzN/ZChbAh5RTFYGQXB5YX05V3gIZU9cViYfAmEqUGIsdXQ+MEo1GmhuO1RRdTV1JhtuCW9SegYLSGxvJEkweG5/aDYN";
})
                  .AddBootstrap5Providers()
                  .AddFontAwesomeIcons();

builder.Services.AddScoped<HtmlRenderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
