using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models.Carrito;
using Proyecto_Final.Models.Pedidos;
using Proyecto_Final.Models.Producto;
using Proyecto_Final.Models.Usuario;

namespace Proyecto_Final.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoItem> CarritoItems { get; set; }
        public DbSet<Valoracion> Valoraciones { get; set; }
        public DbSet<DireccionEnvio> DireccionesEnvio { get; set; }
        public DbSet<ProductoVariacion> ProductoVariaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(p => p.Nombre).HasMaxLength(200).IsRequired();
                entity.Property(p => p.Descripcion).HasMaxLength(1000);
                entity.Property(p => p.Precio).HasColumnType("decimal(18,2)");
                entity.Property(p => p.ImagenUrl).HasMaxLength(500);
                entity.HasIndex(p => p.Nombre);

                entity.HasMany(p => p.Valoraciones)
                    .WithOne(v => v.Producto)
                    .HasForeignKey(v => v.ProductoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.Variaciones)
                    .WithOne(pv => pv.Producto)
                    .HasForeignKey(pv => pv.ProductoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Carrito>()
                .HasMany(c => c.Items)
                .WithOne(ci => ci.Carrito)
                .HasForeignKey(ci => ci.CarritoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarritoItem>(entity =>
            {
                entity.Property(c => c.Cantidad).HasDefaultValue(1);
                entity.Property(c => c.FechaAgregado).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(ci => ci.ProductoVariacion)
                    .WithMany() 
                    .HasForeignKey(ci => ci.ProductoVariacionId)
                    .OnDelete(DeleteBehavior.Cascade); 
            });

            modelBuilder.Entity<Valoracion>(entity =>
            {
                entity.Property(v => v.Comentario).HasMaxLength(1000);
                entity.Property(v => v.Puntuacion).HasColumnType("decimal(3,1)");
                entity.Property(v => v.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<ProductoVariacion>(entity =>
            {
                entity.Property(pv => pv.TipoAtributo).HasMaxLength(50).IsRequired();
                entity.Property(pv => pv.ValorAtributo).HasMaxLength(50).IsRequired();
                entity.Property(pv => pv.Stock).IsRequired();
                entity.Property(pv => pv.PrecioAdicional).HasColumnType("decimal(18,2)");
            });
        }
    }
}