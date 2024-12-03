namespace AdminSeguridad.Models
{
    public class Direccion
    {
        public int DireccionID { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public int DistritoID { get; set; }
        public Distrito Distrito { get; set; }
        public int CantonID { get; set; }
        public Canton Canton { get; set; }
        public int ProvinciaID { get; set; }
        public Provincia Provincia { get; set; }
        public int PaisID { get; set; }
        public Pais Pais { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
    }
}
