using Microsoft.AspNetCore.Identity;
using Proyecto_Final.Models.Pedidos;
using Proyecto_Final.Models.Producto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models.Usuario  // ¡Namespace corregido!
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(100)]
        public string NombreCompleto { get; set; }

        [MaxLength(200)]
        public string Direccion { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Relaciones
        public List<DireccionEnvio> Direcciones { get; set; } = new();
        public List<Pedido> Pedidos { get; set; } = new();
        public List<CarritoItem> CarritoItems { get; set; } = new();  // Añadido para completar
        public List<Valoracion> Valoraciones { get; set; } = new();  // Añadido para completar
    }
}