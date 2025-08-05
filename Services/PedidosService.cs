using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Models.Pedidos; // Se ha actualizado el namespace
using Proyecto_Final.Services;
using Microsoft.Extensions.Logging;

namespace Proyecto_Final.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly ILogger<PedidoService> _logger;

        public PedidoService(IDbContextFactory<ApplicationDbContext> contextFactory, ILogger<PedidoService> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene una lista de pedidos en un rango de fechas de creación opcional.
        /// </summary>
        /// <param name="startDate">La fecha de inicio para el filtro (opcional).</param>
        /// <param name="endDate">La fecha de fin para el filtro (opcional).</param>
        /// <returns>Una lista de pedidos que coinciden con el rango de fechas.</returns>
        public async Task<List<Pedido>> ObtenerPedidosEnRangoDeFechas(DateTime? startDate, DateTime? endDate)
        {
            await using var _context = await _contextFactory.CreateDbContextAsync();
            try
            {
                var query = _context.Pedidos.AsQueryable();

                if (startDate.HasValue)
                {
                    // Se usa la propiedad FechaPedido del modelo
                    query = query.Where(p => p.FechaPedido >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    // Agrega un día para incluir la fecha final completa del día
                    // Se usa la propiedad FechaPedido del modelo
                    query = query.Where(p => p.FechaPedido <= endDate.Value.AddDays(1));
                }

                // Incluimos los detalles del pedido usando la propiedad 'Detalles'
                return await query
                    .Include(p => p.Detalles)
                    .OrderByDescending(p => p.FechaPedido) // Se usa la propiedad FechaPedido
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedidos en rango de fechas.");
                return new List<Pedido>();
            }
        }
    }
}
