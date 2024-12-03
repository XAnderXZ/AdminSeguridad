namespace AdminSeguridad.Models
{
    public class MenuPermiso
    {
        public int MenuID { get; set; }
        public Menu Menu { get; set; }
        public int PermisoID { get; set; }
        public Permiso Permiso { get; set; }
    }
}
