using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models.Pedidos;
using Proyecto_Final.Models.Producto;
using Proyecto_Final.Models.Usuario;

namespace Proyecto_Final.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSets para cada entidad
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<CarritoItem> CarritoItems { get; set; }
        public DbSet<Valoracion> Valoraciones { get; set; }
        public DbSet<DireccionEnvio> DireccionesEnvio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetallePedido>()
                .HasOne(d => d.Producto)
                .WithMany(p => p.DetallesPedido)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración para SQL Server
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DetallePedido>()
                .Property(d => d.PrecioUnitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Total)
                .HasColumnType("decimal(18,2)");

            // Índices para mejorar rendimiento
            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.Nombre);

            modelBuilder.Entity<Pedido>()
                .HasIndex(p => p.FechaPedido);
        }
    }
}