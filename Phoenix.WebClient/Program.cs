using DevExpress.Blazor;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509.Qualified;
using Microsoft.EntityFrameworkCore;
using Phoenix.Infrastructure;
using Phoenix.WebClient.Components;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<PhoenixDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Phoenix"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddScoped<ISampleService, SampleService>();
builder.Services.AddScoped<ToastService>();
builder.Services.AddDevExpressBlazor(configure => configure.BootstrapVersion = BootstrapVersion.v5);
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
