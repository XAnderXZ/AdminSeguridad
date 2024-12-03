namespace AdminSeguridad.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string usuario {  get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
        public int RolID { get; set; }
        public Rol Rol { get; set; }
    }
}
