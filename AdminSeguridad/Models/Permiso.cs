namespace AdminSeguridad.Models
{
    public class Permiso
    {
        public int PermisoID { get; set; }
        public string NombrePermiso { get; set; }
        public bool PermisoLectura { get; set; }
        public bool PermisoEscritura { get; set; }
        public bool PermisoModificacion { get; set; }
        public bool PermisoEliminacion { get; set; }
        public ICollection<RolPermiso> RolPermisos { get; set; }
    }
}
