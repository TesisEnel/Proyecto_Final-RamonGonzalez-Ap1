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

        private async Task<Carrito> GetCarritoAsync(string userId)
        {
            var carrito = await _context.Carritos
                .Include(c => c.Items)
                .ThenInclude(ci => ci.ProductoVariacion)
                .ThenInclude(pv => pv.Producto)
                .FirstOrDefaultAsync(c => c.UsuarioId == userId);

            if (carrito == null)
            {
                carrito = new Carrito
                {
                    UsuarioId = userId,
                    Items = new List<CarritoItem>()
                };
                _context.Carritos.Add(carrito);
                await _context.SaveChangesAsync();
            }

            return carrito;
        }

        public async Task<List<CarritoItem>> ObtenerItemsDelCarritoAsync(string userId)
        {
            var carrito = await GetCarritoAsync(userId);
            return carrito.Items.ToList();
        }

        public async Task AgregarOActualizarItemAsync(string userId, int productoVariacionId, int cantidad)
        {
            var carrito = await GetCarritoAsync(userId);
            var carritoItem = carrito.Items
                .FirstOrDefault(ci => ci.ProductoVariacionId == productoVariacionId);

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

        public async Task ActualizarCantidadAsync(int itemId, int newQuantity)
        {
            var item = await _context.CarritoItems.FindAsync(itemId);
            if (item != null)
            {
                item.Cantidad = newQuantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarDelCarritoAsync(int itemId)
        {
            var item = await _context.CarritoItems.FindAsync(itemId);
            if (item != null)
            {
                _context.CarritoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task LimpiarCarritoAsync(string userId)
        {
            var carrito = await GetCarritoAsync(userId);
            _context.CarritoItems.RemoveRange(carrito.Items);
            await _context.SaveChangesAsync();
        }
    }
}