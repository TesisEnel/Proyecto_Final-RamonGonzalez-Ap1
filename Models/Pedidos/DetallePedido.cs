using Proyecto_Final.Models.Producto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Models.Pedidos
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(18, 2)")] 
        public decimal PrecioUnitario { get; set; }

        public Pedido? Pedido { get; set; }
        public Producto.Producto? Producto { get; set; }
    }
}