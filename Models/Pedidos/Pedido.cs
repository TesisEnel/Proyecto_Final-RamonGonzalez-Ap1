using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Proyecto_Final.Models.Usuario;
using Proyecto_Final.Models.Producto;

namespace Proyecto_Final.Models.Pedidos
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual ApplicationUser? Usuario { get; set; }

        [Required]
        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required]
        [MaxLength(50)]
        public string Estado { get; set; } = "Pendiente";

        public string NombreEnvio { get; set; } = string.Empty;
        public string ApellidoEnvio { get; set; } = string.Empty;
        public string CalleEnvio { get; set; } = string.Empty;
        public string CiudadEnvio { get; set; } = string.Empty;
        public string EstadoEnvio { get; set; } = string.Empty;
        public string CodigoPostalEnvio { get; set; } = string.Empty;
        public string PaisEnvio { get; set; } = string.Empty;
        public string TelefonoEnvio { get; set; } = string.Empty;
        public virtual ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}