namespace AdminSeguridad.Models
{
    public class Area
    {
        public int CodigoArea { get; set; } 
        public string NombreArea { get; set; } 
        public ICollection<Telefono> Telefonos { get; set; }
    }
}
