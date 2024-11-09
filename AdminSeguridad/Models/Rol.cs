namespace AdminSeguridad.Models
{
    public class Rol
    {
        public int RolID { get; set; }
        public string NombreRol { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<RolPermiso> RolPermisos { get; set; }
    }
}
