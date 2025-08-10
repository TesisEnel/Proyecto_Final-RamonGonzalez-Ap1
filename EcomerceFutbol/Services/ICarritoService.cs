using Proyecto_Final.Models.Carrito;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Final.Services
{
    public interface ICarritoService
    {
        Task<List<CarritoItem>> ObtenerItemsDelCarritoAsync(string? userId, string? sessionId);
        Task AgregarOActualizarItemAsync(string? userId, string? sessionId, int productoVariacionId, int cantidad);
        Task ActualizarCantidadAsync(int itemId, int newQuantity, string? userId, string? sessionId);
        Task EliminarDelCarritoAsync(int itemId, string? userId, string? sessionId);
        Task LimpiarCarritoAsync(string? userId, string? sessionId);
        Task UnirCarritosAsync(string? loggedInUserId, string? sessionId);
    }
}