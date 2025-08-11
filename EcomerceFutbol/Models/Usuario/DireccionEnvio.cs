using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Proyecto_Final.Models.Usuario;

namespace Proyecto_Final.Models.Usuario
{
    public class DireccionEnvio
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public ApplicationUser Usuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "El formato del teléfono no es válido. Ej: 809-123-4567")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La calle es obligatoria.")]
        [MaxLength(100, ErrorMessage = "La calle no puede exceder los 100 caracteres.")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        [MaxLength(50, ErrorMessage = "La ciudad no puede exceder los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "La ciudad solo puede contener letras y espacios.")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "La provincia es obligatoria.")]
        [MaxLength(50, ErrorMessage = "La provincia no puede exceder los 50 caracteres.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El código postal no puede exceder los 20 caracteres.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El código postal solo puede contener números.")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "El país es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El país no puede exceder los 50 caracteres.")]
        public string Pais { get; set; } = "República Dominicana";

        public bool EsPrincipal { get; set; }
    }
}