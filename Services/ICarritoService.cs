using Proyecto_Final.Models.Carrito;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Final.Services
{
    public interface ICarritoService
    {
        Task<List<CarritoItem>> ObtenerItemsDelCarritoAsync(string userId);
        Task AgregarOActualizarItemAsync(string userId, int productoVariacionId, int cantidad);
        Task ActualizarCantidadAsync(int itemId, int newQuantity);
        Task EliminarDelCarritoAsync(int itemId);
        Task LimpiarCarritoAsync(string userId);
    }
}