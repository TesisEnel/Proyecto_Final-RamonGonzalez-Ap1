using Proyecto_Final.Models.Usuario;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Models.Carrito
{
    public class Carrito
    {
        [Key]
        public int Id { get; set; }

        public string? UsuarioId { get; set; }

        public string? SessionId { get; set; }

        [ForeignKey("UsuarioId")]
        public ApplicationUser? Usuario { get; set; }

        public ICollection<CarritoItem> Items { get; set; } = new List<CarritoItem>();
    }
}