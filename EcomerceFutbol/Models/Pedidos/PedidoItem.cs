using Proyecto_Final.Models.Pedidos;
using Proyecto_Final.Models.Producto;

namespace Proyecto_Final.Models.Carrito
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int ProductoId { get; set; }
        public ProductoVariacion Producto { get; set; }
        public int ProductoVariacionId { get; set; }
        public ProductoVariacion ProductoVariacion { get; set; }
        public string NombreProducto { get; set; }
        public string ValorAtributo { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}
