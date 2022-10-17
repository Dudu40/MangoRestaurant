using Microsoft.EntityFrameworkCore;
using Mango.Services.Identity;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Mango.Services.Identity.Intializer;
using Mango.Services.Identity.Initializer;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

var identityOptions = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(Utils.GetIdentityResources())
.AddInMemoryApiScopes(Utils.GetApiScope())
.AddInMemoryClients(Utils.GetClients())
.AddAspNetIdentity<ApplicationUser>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
identityOptions.AddDeveloperSigningCredential();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseIdentityServer();
app.UseAuthorization();
//initialize
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    // use dbInitializer
    dbInitializer.Initialize();
}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
