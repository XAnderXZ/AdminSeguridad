namespace AdminSeguridad.Models
{
    public class RolPermiso
    {
        public int RolID { get; set; }
        public Rol Rol { get; set; }
        public int PermisoID { get; set; }
        public Permiso Permiso { get; set; }
    }
}
