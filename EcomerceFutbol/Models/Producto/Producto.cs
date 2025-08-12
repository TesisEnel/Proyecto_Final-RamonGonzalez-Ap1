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

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder los 200 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'&áéíóúÁÉÍÓÚñÑüÜ-]*$", ErrorMessage = "El nombre contiene caracteres no permitidos.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres.")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "La descripción no puede contener los caracteres < o >.")]
        public string Descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 1000000.00, ErrorMessage = "El precio debe ser un valor positivo.")]
        public decimal Precio { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal ValoracionPromedio { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        [Range(0, 1, ErrorMessage = "El descuento debe ser un valor entre 0 y 1 (ej. 0.15 para 15%).")]
        public decimal Descuento { get; set; } = 0;

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]*$", ErrorMessage = "La categoría solo puede contener letras.")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "La imagen del producto es obligatoria.")]
        public string ImagenUrl { get; set; }

        public bool EsNovedad { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public bool TieneVariaciones { get; set; } = false;
        public List<ProductoVariacion> Variaciones { get; set; } = new();
        public List<DetallePedido> DetallesPedido { get; set; } = new();
        public List<Valoracion> Valoraciones { get; set; } = new();
    }
}