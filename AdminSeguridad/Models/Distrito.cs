namespace AdminSeguridad.Models
{
    public class Distrito
    {
        public int DistritoID { get; set; }
        public string NombreDistrito { get; set; }
        public int CantonID { get; set; }
        public Canton Canton { get; set; }
    }
}
