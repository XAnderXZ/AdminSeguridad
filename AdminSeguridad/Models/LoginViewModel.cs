
using System.ComponentModel.DataAnnotations;

namespace AdminSeguridad.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder los 50 caracteres.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Clave { get; set; }

        // Esta propiedad se usa para indicar si hubo un error de inicio de sesión
        public string ErrorMessage { get; set; }
    }
}

