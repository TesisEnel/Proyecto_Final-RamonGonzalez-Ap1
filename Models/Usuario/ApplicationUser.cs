using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Proyecto_Final.Models.Pedidos;
using Proyecto_Final.Models.Producto;
using Proyecto_Final.Models.Carrito;

namespace Proyecto_Final.Models.Usuario
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio."), MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio."), MaxLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public List<DireccionEnvio> Direcciones { get; set; } = new List<DireccionEnvio>();
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public List<CarritoItem> CarritoItems { get; set; } = new List<CarritoItem>();
        public List<Valoracion> Valoraciones { get; set; } = new List<Valoracion>();

        public ApplicationUser()
        {
        }
    }
}