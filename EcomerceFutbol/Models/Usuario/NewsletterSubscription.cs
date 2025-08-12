using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models
{
    public class NewsletterSubscription
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; } = string.Empty;

        public DateTime SubscriptionDate { get; set; } = DateTime.UtcNow;
    }
}