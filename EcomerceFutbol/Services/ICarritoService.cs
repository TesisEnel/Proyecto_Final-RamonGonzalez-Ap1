using Proyecto_Final.Models.Carrito;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Final.Services
{
    public interface ICarritoService
    {
        // El método ahora acepta un userId O un sessionId, ambos son opcionales
        Task<List<CarritoItem>> ObtenerItemsDelCarritoAsync(string? userId, string? sessionId);

        // El método para agregar también acepta ambos, userId o sessionId
        Task AgregarOActualizarItemAsync(string? userId, string? sessionId, int productoVariacionId, int cantidad);

        // Los métodos de eliminación y actualización deben poder identificar el carrito del invitado o del usuario
        Task ActualizarCantidadAsync(int itemId, int newQuantity, string? userId, string? sessionId);
        Task EliminarDelCarritoAsync(int itemId, string? userId, string? sessionId);
        Task LimpiarCarritoAsync(string? userId, string? sessionId);
    }
}