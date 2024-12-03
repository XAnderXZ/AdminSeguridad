namespace AdminSeguridad.Models
{
    public class Canton
    {
        public int CantonID { get; set; }
        public string NombreCanton { get; set; }
        public int ProvinciaID { get; set; }
        public Provincia Provincia { get; set; }
        public ICollection<Distrito> Distritos { get; set; }
    }
}
