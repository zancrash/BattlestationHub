using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BattlestationHub.Data;
using BattlestationHub.Models;
using Microsoft.Extensions.Azure;

var options = new WebApplicationOptions { EnvironmentName = "Production" };
var builder = WebApplication.CreateBuilder(options);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Azure") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// Configuration for Blob Storage
builder.Services.Configure<BlobStorageSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<string>(builder.Configuration.GetSection("BlobContainerName"));


builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequiredLength = 8;                    // Minimum password length
        options.Password.RequiredUniqueChars = 3;               // Minimum number of unique characters
        options.Password.RequireNonAlphanumeric = false;       // Require non-alphanumeric characters
        options.Password.RequireUppercase = false;              // Require uppercase letters
        options.Password.RequireLowercase = false;              // Require lowercase letters
        options.Password.RequireDigit = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["ConnectionStrings:BlobStorage:blob"], preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["ConnectionStrings:BlobStorage:queue"], preferMsi: true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseMigrationsEndPoint();
}
else
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

app.MapRazorPages();
app.Run();
