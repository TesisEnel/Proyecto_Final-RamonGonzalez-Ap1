using Proyecto_Final.Models.Usuario;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Models.Producto
{
    public class Valoracion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Range(1, 5)]
        public decimal Puntuacion { get; set; }

        [MaxLength(1000)]
        public string Comentario { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public virtual ApplicationUser Usuario { get; set; }
        public virtual Producto Producto { get; set; }
    }
}