using MCBABackend.Contexts;
using MCBABackend.Services;
using MCBACustomerApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<McbaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"), b => b.MigrationsAssembly(nameof(MCBACustomerApi)));
});

// Repositories
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<LoginRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Time to seed the data from the web service
// Initialise DB
using (var scope = app.Services.CreateScope())
{
    try
    {
        DataInitialiseService.RetrieveAndSave(scope.ServiceProvider, builder.Configuration.GetConnectionString("DatabaseInit"));
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}


app.Run();
