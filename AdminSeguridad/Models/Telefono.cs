namespace AdminSeguridad.Models
{
    public class Telefono
    {
        public int TelefonoID { get; set; } 
        public int CodigoArea { get; set; } 
        public string Numero { get; set; } 
        public Area Area { get; set; }
    }
}
