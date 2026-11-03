using System.Net;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TestMVCSharpC.Data;
using TestMVCSharpC.Filters;
using TestMVCSharpC.Models;
using TestMVCSharpC.Repositories.Implementation;
using TestMVCSharpC.Repositories.Interface;
using TestMVCSharpC.Services.Implementation;
using TestMVCSharpC.Services.Interface;
using TestMVCSharpC.Utils;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TestMVCSharpCContext>(options =>
    options.UseNpgsql(connectionString));


// Adding repositories ...
builder.Services.AddScoped<IAdresseRepository, AdresseRepository>();
builder.Services.AddScoped<IParticiperRepository, ParticiperRepository>();
builder.Services.AddScoped<IPasseportRepository, PasseportRepository>();
builder.Services.AddScoped<IPersonneRepository, PersonneRepository>();
builder.Services.AddScoped<IProjetRepository, ProjetRepository>();
builder.Services.AddScoped<IVFicheCompletePersonneRepository, VFicheCompletePersonneRepository>();
builder.Services.AddScoped<IVProjetsEtEquipeRepository, VProjetsEtEquipeRepository>();
builder.Services.AddScoped<IVSynthetiseGlobalRepository, VSynthetiseGlobalRepository>();

// Adding services ...
builder.Services.AddScoped<IAdresseService, AdresseService>();
builder.Services.AddScoped<IParticiperService, ParticiperService>();
builder.Services.AddScoped<IPasseportService, PasseportService>();
builder.Services.AddScoped<IPersonneService, PersonneService>();
builder.Services.AddScoped<IProjetService, ProjetService>();
builder.Services.AddScoped<IVFicheCompletePersonneService, VFicheCompletePersonneService>();
builder.Services.AddScoped<IVProjetsEtEquipeService, VProjetsEtEquipeService>();
builder.Services.AddScoped<IVSynthetiseGlobalService, VSynthetiseGlobalService>();

// Adding controllers ...
builder.Services.AddControllersWithViews(options =>
{
    // Adding the global exception filter to all controllers
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddScoped<EntityUtils>();  

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/Error/{0}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/500");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
pattern: "{controller=Adresse}/{action=Index}/{id?}");

app.Run();
