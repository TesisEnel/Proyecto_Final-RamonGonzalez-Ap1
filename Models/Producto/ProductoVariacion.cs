using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Models.Producto
{
    public class ProductoVariacion
    {
        public int Id { get; set; }

        
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; } 

        [Required(ErrorMessage = "El tipo de atributo es obligatorio (ej. Talla, Color).")]
        [StringLength(50, ErrorMessage = "El tipo de atributo no puede exceder los 50 caracteres.")]
        public string TipoAtributo { get; set; } 

        [Required(ErrorMessage = "El valor del atributo es obligatorio (ej. S, M, L, Rojo, Azul).")]
        [StringLength(50, ErrorMessage = "El valor del atributo no puede exceder los 50 caracteres.")]
        public string ValorAtributo { get; set; } 

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.00, 1000000.00, ErrorMessage = "El precio adicional debe ser un valor válido.")]
        public decimal? PrecioAdicional { get; set; }
    }
}
