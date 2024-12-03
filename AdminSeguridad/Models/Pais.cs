namespace AdminSeguridad.Models
{
    public class Pais
    {
        public int PaisID { get; set; }
        public string NombrePais { get; set; }
        public ICollection<Provincia> Provincias { get; set; }
    }
}
