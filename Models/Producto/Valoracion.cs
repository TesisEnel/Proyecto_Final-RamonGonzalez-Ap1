using Proyecto_Final.Models;
using Proyecto_Final.Models.Usuario;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models.Producto

{
    public class Valoracion

    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        [Required]
        public string UsuarioId { get; set; } 
        [Required]
        [Range(1, 5, ErrorMessage = "La puntuación debe estar entre 1 y 5.")]
        public int Puntuacion { get; set; }
        [MaxLength(1000)]
        public string Comentario { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public Producto? Producto { get; set; }
        public ApplicationUser? Usuario { get; set; }

    }

}