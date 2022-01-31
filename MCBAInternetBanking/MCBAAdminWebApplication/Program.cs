using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuring CustomerApi
builder.Services.AddHttpClient(Options.DefaultName, client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("CustomerApi"));
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

// Add this so the views can access it! This gives the HTTP Content (sessions are in there)
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    // Make the session cookie essential.
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
