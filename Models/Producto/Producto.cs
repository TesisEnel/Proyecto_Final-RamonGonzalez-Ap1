using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models.Producto
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Required, Range(0, 9999.99)]
        public decimal Precio { get; set; }

        [Range(0, 100)]
        public int Descuento { get; set; }

        [Required]
        public string Categoria { get; set; } // "Camisetas", "Botines", etc.

        public string ImagenUrl { get; set; }

        [Required]
        public int Stock { get; set; }

        public bool EsNovedad { get; set; }

        public double ValoracionPromedio { get; set; }

        public List<Valoracion> Valoraciones { get; set; } = new();

        public List<DetallePedido> DetallesPedido { get; set; } = new();

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}