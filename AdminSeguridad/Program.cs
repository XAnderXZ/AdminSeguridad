using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using AdminSeguridad.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext para usar SQL Server con la cadena de conexi�n
builder.Services.AddDbContext<AdminSeguridadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdminSeguridadDB")));

// Agregar autenticaci�n con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";          // Ruta para el login
        options.AccessDeniedPath = "/Account/AccessDenied";  // Ruta para acceso denegado
    });

// Agregar autorizaci�n con pol�ticas
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

// Agregar servicios al contenedor (Servicios de MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();  // Habilitar archivos est�ticos

app.UseRouting();  // Habilitar el enrutamiento

// Agregar autenticaci�n y autorizaci�n al pipeline
app.UseAuthentication();  // Habilitar autenticaci�n
app.UseAuthorization();   // Habilitar autorizaci�n

// Configurar las rutas para el controlador por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
