using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using AdminSeguridad.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext para usar SQL Server con la cadena de conexión
builder.Services.AddDbContext<AdminSeguridadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdminSeguridadDB")));

// Agregar autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";          // Ruta para el login
        options.AccessDeniedPath = "/Account/AccessDenied";  // Ruta para acceso denegado
    });

// Agregar autorización con políticas
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

app.UseStaticFiles();  // Habilitar archivos estáticos

app.UseRouting();  // Habilitar el enrutamiento

// Agregar autenticación y autorización al pipeline
app.UseAuthentication();  // Habilitar autenticación
app.UseAuthorization();   // Habilitar autorización

// Configurar las rutas para el controlador por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
