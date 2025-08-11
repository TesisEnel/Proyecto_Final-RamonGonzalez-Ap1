using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Models.Carrito;
using Proyecto_Final.Models.Producto;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Proyecto_Final.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly ApplicationDbContext _context;

        public CarritoService(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<Carrito> GetCarritoAsync(string? userId, string? sessionId)
        {
            if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentException("Se requiere un userId o un sessionId válido para obtener el carrito.");
            }

            var carrito = await _context.Carritos
              .Include(c => c.Items)
              .ThenInclude(ci => ci.ProductoVariacion)
              .ThenInclude(pv => pv.Producto)
              .FirstOrDefaultAsync(c => c.UsuarioId == userId || (string.IsNullOrEmpty(userId) && c.SessionId == sessionId));

            if (carrito == null)
            {
                carrito = new Carrito
                {
                    UsuarioId = userId,
                    SessionId = sessionId,
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

        public async Task UnirCarritosAsync(string? loggedInUserId, string? sessionId)
        {
            if (string.IsNullOrEmpty(loggedInUserId) || string.IsNullOrEmpty(sessionId))
            {
                return;
            }

            var carritos = await _context.Carritos
                .Include(c => c.Items)
                .Where(c => c.UsuarioId == loggedInUserId || c.SessionId == sessionId)
                .ToListAsync();

            var usuarioCarrito = carritos.FirstOrDefault(c => c.UsuarioId == loggedInUserId);
            var sessionCarrito = carritos.FirstOrDefault(c => c.SessionId == sessionId);

            if (sessionCarrito == null || !sessionCarrito.Items.Any())
            {
                return;
            }

            if (usuarioCarrito == null)
            {
                sessionCarrito.UsuarioId = loggedInUserId;
                sessionCarrito.SessionId = null;
            }
            else
            {
                foreach (var sessionItem in sessionCarrito.Items)
                {
                    var existingItem = usuarioCarrito.Items.FirstOrDefault(ui => ui.ProductoVariacionId == sessionItem.ProductoVariacionId);

                    if (existingItem != null)
                    {
                        existingItem.Cantidad += sessionItem.Cantidad;
                    }
                    else
                    {
                        sessionItem.CarritoId = usuarioCarrito.Id;
                        usuarioCarrito.Items.Add(sessionItem);
                    }
                }
                _context.Carritos.Remove(sessionCarrito);
            }
            await _context.SaveChangesAsync();
        }
    }
}