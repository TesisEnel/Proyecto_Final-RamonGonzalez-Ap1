using Microsoft.AspNetCore.Identity;

namespace Proyecto_Final.Data
{
    public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError() => new()
        {
            Code = "DefaultError",
            Description = "Ocurrió un error desconocido."
        };

        public override IdentityError DuplicateEmail(string email) => new()
        {
            Code = "DuplicateEmail",
            Description = $"El email '{email}' ya está registrado."
        };

        public override IdentityError PasswordTooShort(int length) => new()
        {
            Code = "PasswordTooShort",
            Description = $"La contraseña debe tener al menos {length} caracteres."
        };
    }
}