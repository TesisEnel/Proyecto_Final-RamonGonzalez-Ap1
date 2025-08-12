using Proyecto_Final.Models;
using Proyecto_Final.Models.Producto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Final.Services
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerProductosEnRangoDeFechas(DateTime? fechaInicio, DateTime? fechaFin);
        Task<List<Producto>> ObtenerProductosDestacados(int cantidad);
        Task<Producto?> ObtenerProductoPorId(int id);
        Task<List<Producto>> ObtenerProductosPorCategoria(string categoria, int pagina = 1, int cantidadPorPagina = 10);
        Task<List<Producto>> BuscarProductos(string terminoBusqueda, int pagina = 1, int cantidadPorPagina = 10);
        Task<List<Producto>> ObtenerProductosRelacionados(int productoId, int cantidad = 4);
        Task ActualizarValoracionPromedio(int productoId);
        Task<List<Producto>> ObtenerProductosMasVendidos(int cantidad);
        Task<List<Producto>> ObtenerProductosEnOferta(int cantidad);
        Task<bool> CrearProducto(Producto producto);
        Task<bool> ActualizarProducto(Producto producto);
        Task<bool> DesactivarProducto(int productoId);
        Task<ProductoVariacion?> ObtenerProductoVariacionPorIdAsync(int id);
        Task AgregarValoracion(Valoracion valoracion);
    }
}