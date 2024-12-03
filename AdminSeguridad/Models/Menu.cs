namespace AdminSeguridad.Models
{
    public class Menu
    {
        public int MenuID { get; set; }
        public string NombreMenu { get; set; }
        public ICollection<MenuPermiso> MenuPermisos { get; set; }
    }
}
