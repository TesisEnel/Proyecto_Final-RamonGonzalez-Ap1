using Proyecto_Final.Models;
using Proyecto_Final.Models.Usuario;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models.Producto
{
    public class Valoracion
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? CorreoElectronico { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [Required(ErrorMessage = "La puntuación es obligatoria.")]
        [Range(1, 5, ErrorMessage = "La puntuación debe estar entre 1 y 5.")]
        public int Puntuacion { get; set; }

        [Required(ErrorMessage = "La reseña es obligatoria.")]
        [StringLength(500, ErrorMessage = "La reseña no puede exceder los 500 caracteres.")]
        public string Comentario { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public Producto? Producto { get; set; }
        public ApplicationUser? Usuario { get; set; }
    }
}