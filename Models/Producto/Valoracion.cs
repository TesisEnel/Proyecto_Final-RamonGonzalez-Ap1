using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Proyecto_Final.Models.Usuario;

namespace Proyecto_Final.Models.Producto
{
    public class Valoracion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public ApplicationUser Usuario { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        [Required, Range(1, 5)]
        public int Puntuacion { get; set; }

        [MaxLength(500)]
        public string Comentario { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}