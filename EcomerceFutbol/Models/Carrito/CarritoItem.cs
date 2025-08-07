using Proyecto_Final.Models.Producto;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Models.Carrito
{
    public class CarritoItem
    {
        [Key]
        public int Id { get; set; }

        public int Cantidad { get; set; }
        public DateTime FechaAgregado { get; set; }

        public int CarritoId { get; set; }
        [ForeignKey("CarritoId")]
        public virtual Carrito Carrito { get; set; }

        public int ProductoVariacionId { get; set; }
        [ForeignKey("ProductoVariacionId")]
        public virtual ProductoVariacion ProductoVariacion { get; set; }
    }
}