using Proyecto_Final.Models.Pedidos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Models.Producto
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(1000)]
        public string Descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal ValoracionPromedio { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal Descuento { get; set; } = 0;

        [Required]
        public string Categoria { get; set; }

        public string ImagenUrl { get; set; }

        public bool EsNovedad { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public bool TieneVariaciones { get; set; } = false;
        public List<ProductoVariacion> Variaciones { get; set; } = new();
        public List<DetallePedido> DetallesPedido { get; set; } = new();
        public List<Valoracion> Valoraciones { get; set; } = new();
    }
}
