using Proyecto_Final.Models.Usuario;
using System.Collections.Generic;

namespace Proyecto_Final.Models.Carrito
{
    public class Carrito
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public virtual ICollection<CarritoItem> Items { get; set; } = new List<CarritoItem>();
    }
}