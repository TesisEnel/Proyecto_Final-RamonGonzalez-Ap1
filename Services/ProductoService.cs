using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Models.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.Services
{
    public class ProductoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductoService> _logger;

        public ProductoService(ApplicationDbContext context, ILogger<ProductoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Producto>> ObtenerProductosDestacados(int cantidad)
        {
            try
            {
                return await _context.Productos
                    .Where(p => p.EsNovedad) 
                    .Include(p => p.DetallesPedido)
                    .Include(p => p.Valoraciones)
                    .OrderByDescending(p => p.FechaCreacion)
                    .Take(cantidad)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos destacados");
                return new List<Producto>();
            }
        }

        public async Task<Producto?> ObtenerProductoPorId(int id)
        {
            try
            {
                return await _context.Productos
                    .Include(p => p.Valoraciones)
                        .ThenInclude(v => v.Usuario)
                    .Include(p => p.DetallesPedido)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener producto con ID {id}");
                return null;
            }
        }

        public async Task<List<Producto>> ObtenerProductosPorCategoria(string categoria, int pagina = 1, int cantidadPorPagina = 10)
        {
            try
            {
                return await _context.Productos
                    .Where(p => p.Categoria == categoria)
                    .Include(p => p.Valoraciones)
                    .OrderBy(p => p.Nombre)
                    .Skip((pagina - 1) * cantidadPorPagina)
                    .Take(cantidadPorPagina)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener productos por categoría {categoria}");
                return new List<Producto>();
            }
        }

        public async Task<List<Producto>> BuscarProductos(string terminoBusqueda, int pagina = 1, int cantidadPorPagina = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(terminoBusqueda))
                    return new List<Producto>();

                return await _context.Productos
                    .Where(p => p.Nombre.Contains(terminoBusqueda) ||
                               p.Descripcion.Contains(terminoBusqueda) ||
                               p.Categoria.Contains(terminoBusqueda))
                    .Include(p => p.Valoraciones)
                    .OrderBy(p => p.Nombre)
                    .Skip((pagina - 1) * cantidadPorPagina)
                    .Take(cantidadPorPagina)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al buscar productos con término '{terminoBusqueda}'");
                return new List<Producto>();
            }
        }

        public async Task<List<Producto>> ObtenerProductosRelacionados(int productoId, int cantidad = 4)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(productoId);
                if (producto == null)
                    return new List<Producto>();

                return await _context.Productos
                    .Where(p => p.Categoria == producto.Categoria && p.Id != productoId)
                    .Include(p => p.Valoraciones)
                    .OrderByDescending(p => p.FechaCreacion)
                    .Take(cantidad)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener productos relacionados para producto {productoId}");
                return new List<Producto>();
            }
        }

        public async Task ActualizarValoracionPromedio(int productoId)
        {
            try
            {
                var producto = await _context.Productos
                    .Include(p => p.Valoraciones)
                    .FirstOrDefaultAsync(p => p.Id == productoId);

                if (producto != null)
                {
                    producto.ValoracionPromedio = producto.Valoraciones.Any() ?
                        (decimal)producto.Valoraciones.Average(v => v.Puntuacion) : 0m;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar valoración promedio para producto {productoId}");
            }
        }

        public async Task<List<Producto>> ObtenerProductosMasVendidos(int cantidad)
        {
            try
            {
                return await _context.Productos
                    .Include(p => p.DetallesPedido)
                    .Include(p => p.Valoraciones)
                    .OrderByDescending(p => p.DetallesPedido.Sum(d => d.Cantidad))
                    .Take(cantidad)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos más vendidos");
                return new List<Producto>();
            }
        }

        public async Task<List<Producto>> ObtenerProductosEnOferta(int cantidad)
        {
            try
            {
                return await _context.Productos
                    .Where(p => p.Descuento > 0)
                    .Include(p => p.Valoraciones)
                    .OrderByDescending(p => p.Descuento)
                    .Take(cantidad)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos en oferta");
                return new List<Producto>();
            }
        }

        public async Task<bool> CrearProducto(Producto producto)
        {
            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto");
                return false;
            }
        }

        public async Task<bool> ActualizarProducto(Producto producto)
        {
            try
            {
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar producto {producto.Id}");
                return false;
            }
        }

        public async Task<bool> DesactivarProducto(int productoId)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(productoId);
                if (producto == null) return false;
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar producto {productoId}");
                return false;
            }
        }
    }
}