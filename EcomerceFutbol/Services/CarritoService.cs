using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Models.Carrito;
using Proyecto_Final.Models.Producto;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Proyecto_Final.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarritoService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<Carrito> GetCarritoAsync(string? userId, string? sessionId)
        {
            // Busca un carrito por userId o por sessionId.
            var carrito = await _context.Carritos
                .Include(c => c.Items)
                .ThenInclude(ci => ci.ProductoVariacion)
                .ThenInclude(pv => pv.Producto)
                .FirstOrDefaultAsync(c => c.UsuarioId == userId || (!string.IsNullOrEmpty(sessionId) && c.SessionId == sessionId));

            if (carrito == null)
            {
                // Si no se encuentra un carrito, crea uno nuevo.
                // Elige el userId o el sessionId en función de cuál está presente.
                carrito = new Carrito
                {
                    UsuarioId = userId,
                    SessionId = string.IsNullOrEmpty(userId) ? sessionId : null,
                    Items = new List<CarritoItem>()
                };
                _context.Carritos.Add(carrito);
                await _context.SaveChangesAsync();
            }
            return carrito;
        }

        public async Task<List<CarritoItem>> ObtenerItemsDelCarritoAsync(string? userId, string? sessionId)
        {
            var carrito = await GetCarritoAsync(userId, sessionId);
            return carrito.Items.ToList();
        }

        public async Task AgregarOActualizarItemAsync(string? userId, string? sessionId, int productoVariacionId, int cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor que cero.");
            }

            var carrito = await GetCarritoAsync(userId, sessionId);
            var carritoItem = carrito.Items.FirstOrDefault(ci => ci.ProductoVariacionId == productoVariacionId);

            if (carritoItem == null)
            {
                var productoVariacion = await _context.ProductoVariaciones
                    .Include(pv => pv.Producto)
                    .FirstOrDefaultAsync(pv => pv.Id == productoVariacionId);

                if (productoVariacion == null)
                {
                    throw new InvalidOperationException("La variación del producto no existe.");
                }

                carritoItem = new CarritoItem
                {
                    CarritoId = carrito.Id,
                    ProductoVariacionId = productoVariacion.Id,
                    Cantidad = cantidad,
                    FechaAgregado = DateTime.UtcNow
                };
                _context.CarritoItems.Add(carritoItem);
            }
            else
            {
                carritoItem.Cantidad += cantidad;
            }
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarCantidadAsync(int itemId, int newQuantity, string? userId, string? sessionId)
        {
            if (newQuantity <= 0)
            {
                throw new ArgumentException("La nueva cantidad debe ser mayor que cero.");
            }

            var carrito = await GetCarritoAsync(userId, sessionId);
            var item = carrito.Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                item.Cantidad = newQuantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("El item a actualizar no pertenece a este carrito.");
            }
        }

        public async Task EliminarDelCarritoAsync(int itemId, string? userId, string? sessionId)
        {
            var carrito = await GetCarritoAsync(userId, sessionId);
            var item = carrito.Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                _context.CarritoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("El item a eliminar no pertenece a este carrito.");
            }
        }

        public async Task LimpiarCarritoAsync(string? userId, string? sessionId)
        {
            var carrito = await GetCarritoAsync(userId, sessionId);
            if (carrito.Items.Any())
            {
                _context.CarritoItems.RemoveRange(carrito.Items);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnirCarritosAsync(string? loggedInUserId)
        {
            if (string.IsNullOrEmpty(loggedInUserId))
            {
                return;
            }

            string? currentSessionId = _httpContextAccessor.HttpContext?.Session.Id;
            if (string.IsNullOrEmpty(currentSessionId))
            {
                return;
            }

            var carritos = await _context.Carritos
                .Include(c => c.Items)
                .Where(c => c.UsuarioId == loggedInUserId || c.SessionId == currentSessionId)
                .ToListAsync();

            var usuarioCarrito = carritos.FirstOrDefault(c => c.UsuarioId == loggedInUserId);
            var sessionCarrito = carritos.FirstOrDefault(c => c.SessionId == currentSessionId);

            if (sessionCarrito == null || !sessionCarrito.Items.Any())
            {
                return;
            }

            if (usuarioCarrito == null)
            {
                // Si el usuario no tiene carrito, asigna el de la sesión
                sessionCarrito.UsuarioId = loggedInUserId;
                sessionCarrito.SessionId = null;
            }
            else
            {
                // Si el usuario tiene carrito, fusiona los ítems
                foreach (var sessionItem in sessionCarrito.Items)
                {
                    var existingItem = usuarioCarrito.Items.FirstOrDefault(ui => ui.ProductoVariacionId == sessionItem.ProductoVariacionId);

                    if (existingItem != null)
                    {
                        existingItem.Cantidad += sessionItem.Cantidad;
                    }
                    else
                    {
                        // Mueve el ítem del carrito de sesión al de usuario
                        sessionItem.CarritoId = usuarioCarrito.Id;
                        usuarioCarrito.Items.Add(sessionItem);
                    }
                }
                // Elimina el carrito de sesión
                _context.Carritos.Remove(sessionCarrito);
            }
            await _context.SaveChangesAsync();
        }
    }
}