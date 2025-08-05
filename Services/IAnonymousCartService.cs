using Proyecto_Final.Models.Carrito;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Final.Services
{
    public interface IAnonymousCartService
    {
        Task AddToAnonymousCartAsync(int productoVariacionId, int cantidad);
        Task<List<CarritoItem>> GetAnonymousCartItemsAsync();
        Task UpdateAnonymousCartItemAsync(int productoVariacionId, int cantidad);
        Task RemoveFromAnonymousCartAsync(int productoVariacionId);
        Task ClearAnonymousCartAsync();
    }
}