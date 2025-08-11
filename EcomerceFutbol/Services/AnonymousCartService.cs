using Proyecto_Final.Models.Carrito;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace Proyecto_Final.Services
{
    public class AnonymousCartService : IAnonymousCartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductoService _productoService;
        private const string CartSessionKey = "AnonymousCart";

        public AnonymousCartService(IHttpContextAccessor httpContextAccessor, IProductoService productoService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productoService = productoService;
        }

        public async Task AddToAnonymousCartAsync(int productoVariacionId, int cantidad)
        {
            var cartItems = GetAnonymousCartItemsFromSession();
            var existingItem = cartItems.FirstOrDefault(i => i.ProductoVariacionId == productoVariacionId);

            if (existingItem != null)
            {
                existingItem.Cantidad += cantidad;
            }
            else
            {
                var productoVariacion = await _productoService.ObtenerProductoVariacionPorIdAsync(productoVariacionId);
                if (productoVariacion != null)
                {
                    cartItems.Add(new CarritoItem
                    {
                        ProductoVariacionId = productoVariacionId,
                        Cantidad = cantidad,
                        ProductoVariacion = productoVariacion
                    });
                }
            }
            SetAnonymousCartItemsToSession(cartItems);
        }

        public async Task<List<CarritoItem>> GetAnonymousCartItemsAsync()
        {
            var cartItems = GetAnonymousCartItemsFromSession();
            foreach (var item in cartItems)
            {
                if (item.ProductoVariacion == null)
                {
                    var productoVariacion = await _productoService.ObtenerProductoVariacionPorIdAsync(item.ProductoVariacionId);
                    if (productoVariacion != null)
                    {
                        item.ProductoVariacion = productoVariacion;
                    }
                }
            }
            return cartItems;
        }

        public async Task UpdateAnonymousCartItemAsync(int productoVariacionId, int cantidad)
        {
            var cartItems = GetAnonymousCartItemsFromSession();
            var itemToUpdate = cartItems.FirstOrDefault(i => i.ProductoVariacionId == productoVariacionId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Cantidad = cantidad;
                SetAnonymousCartItemsToSession(cartItems);
            }
            await Task.CompletedTask;
        }

        public async Task RemoveFromAnonymousCartAsync(int productoVariacionId)
        {
            var cartItems = GetAnonymousCartItemsFromSession();
            var itemToRemove = cartItems.FirstOrDefault(i => i.ProductoVariacionId == productoVariacionId);
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                SetAnonymousCartItemsToSession(cartItems);
            }
            await Task.CompletedTask;
        }

        public async Task ClearAnonymousCartAsync()
        {
            _httpContextAccessor.HttpContext?.Session.Remove(CartSessionKey);
            await Task.CompletedTask;
        }

        private List<CarritoItem> GetAnonymousCartItemsFromSession()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                return new List<CarritoItem>();
            }
            var cartData = session.GetString(CartSessionKey);
            return cartData == null ? new List<CarritoItem>() : JsonSerializer.Deserialize<List<CarritoItem>>(cartData);
        }

        private void SetAnonymousCartItemsToSession(List<CarritoItem> cartItems)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session != null)
            {
                session.SetString(CartSessionKey, JsonSerializer.Serialize(cartItems));
            }
        }
    }
}