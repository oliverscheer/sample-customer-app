using Customer.BusinessLogic.Database.Repositories;
using Customer.BusinessLogic.Services.Country;
using Customer.DatabaseLogic;
using Customer.Web.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();
builder.Services.AddRadzenComponents();

string connectionString = builder.Configuration.GetConnectionString("sqldb"); 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddTransient<CountryService>();
builder.Services.AddTransient<CountryRepository>();

// TODO: Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Customer.Web.Client._Imports).Assembly);


app.MapGet("/createdb", async (AppDbContext context) =>
{
    // You wouldn't normally do this on every call,
    // but doing it here just to make this simple.
    context.Database.EnsureCreated();

    return new
    {
        status = "db created"
    };
});


app.MapGet("/dropdb", async (AppDbContext context) =>
{
    // You wouldn't normally do this on every call,
    // but doing it here just to make this simple.
    context.Database.EnsureDeleted();

    return new
    {
        status = "db deleted"
    };
});

app.Run();
