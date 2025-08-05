using Proyecto_Final.Models.Pedidos;

namespace Proyecto_Final.Services
{
    public interface IPedidoService
    {
        Task<List<Pedido>> ObtenerPedidosEnRangoDeFechas(DateTime? startDate, DateTime? endDate);
    }
}
