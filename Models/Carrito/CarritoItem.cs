using Proyecto_Final.Models.Producto;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Models.Carrito
{
    public class CarritoItem
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaAgregado { get; set; } = DateTime.UtcNow;
        public int CarritoId { get; set; }
        public virtual Carrito Carrito { get; set; }
        public int ProductoVariacionId { get; set; }
        public virtual ProductoVariacion ProductoVariacion { get; set; }
        public int ProductoId { get; set; }
        public virtual Proyecto_Final.Models.Producto.Producto Producto { get; set; }
    }
}