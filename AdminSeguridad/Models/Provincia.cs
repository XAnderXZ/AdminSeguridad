namespace AdminSeguridad.Models
{
    public class Provincia
    {
        public int ProvinciaID { get; set; }
        public string NombreProvincia { get; set; }
        public int PaisID { get; set; }
        public Pais Pais { get; set; }
        public ICollection<Canton> Cantones { get; set; }
    }
}
