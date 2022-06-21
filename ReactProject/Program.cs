using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Azure;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000", "https://appname.azurestaticapp.net");
    });
});

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddAzureClients(builder =>
{
    builder.AddClient<ServiceBusClient, ServiceBusClientOptions>((_, _, _) =>
    {
        return new ServiceBusClient("nixons.servicebus.windows.net", new DefaultAzureCredential());
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("CORSPolicy");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
