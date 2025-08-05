using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Proyecto_Final.Models.Usuario;

namespace Proyecto_Final.Models.Usuario
{
    public class DireccionEnvio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public ApplicationUser Usuario { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [Required, MaxLength(100)]
        public string Apellido { get; set; }

        [Required, MaxLength(20)]
        public string Telefono { get; set; }

        [Required, MaxLength(100)]
        public string Calle { get; set; }

        [Required, MaxLength(50)]
        public string Ciudad { get; set; }

        [Required, MaxLength(50)]
        public string Estado { get; set; }

        [Required, MaxLength(20)]
        public string CodigoPostal { get; set; }

        [Required, MaxLength(50)]
        public string Pais { get; set; } = "República Dominicana";

        public bool EsPrincipal { get; set; }
    }
}