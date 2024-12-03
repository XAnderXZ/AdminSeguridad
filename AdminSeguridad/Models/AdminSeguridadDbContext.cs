using Microsoft.EntityFrameworkCore;

namespace AdminSeguridad.Models
{
    public class AdminSeguridadDbContext : DbContext
    {
        public AdminSeguridadDbContext(DbContextOptions<AdminSeguridadDbContext> options) : base(options) { }

        public DbSet<Pais> Paises { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Canton> Cantones { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }
        public DbSet<MenuPermiso> MenuPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración explícita de la clave primaria de 'Area'
            modelBuilder.Entity<Area>()
                .HasKey(a => a.CodigoArea);  // Aseguramos que 'CodigoArea' sea la clave primaria

            // Relación entre Area y Telefono (uno a muchos)
            modelBuilder.Entity<Telefono>()
                .HasOne(t => t.Area)
                .WithMany(a => a.Telefonos)
                .HasForeignKey(t => t.CodigoArea);

            // Relación entre Pais y Provincia (uno a muchos)
            modelBuilder.Entity<Provincia>()
                .HasOne(p => p.Pais)
                .WithMany(p => p.Provincias)
                .HasForeignKey(p => p.PaisID);

            // Relación entre Provincia y Canton (uno a muchos)
            modelBuilder.Entity<Canton>()
                .HasOne(c => c.Provincia)
                .WithMany(p => p.Cantones)
                .HasForeignKey(c => c.ProvinciaID);

            // Relación muchos a muchos entre Rol y Permiso (a través de RolPermiso)
            modelBuilder.Entity<RolPermiso>()
                .HasKey(rp => new { rp.RolID, rp.PermisoID });  // Clave primaria compuesta

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Rol)
                .WithMany(r => r.RolPermisos)
                .HasForeignKey(rp => rp.RolID);

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Permiso)
                .WithMany(p => p.RolPermisos)
                .HasForeignKey(rp => rp.PermisoID);

            modelBuilder.Entity<MenuPermiso>()
                .HasKey(mp => new { mp.MenuID, mp.PermisoID });

            base.OnModelCreating(modelBuilder);
        }
    }
}

