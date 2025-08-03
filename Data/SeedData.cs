using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Proyecto_Final.Models.Usuario;

namespace Proyecto_Final.Data
{
    public class SeedData  
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Initialize()  
        {
            try
            {
                // 1. Crear Roles si no existen
                await EnsureRolesCreated();

                // 2. Crear Usuario Admin por defecto
                await EnsureAdminUserExists();

                // 3. Crear Usuario Cliente de prueba
                await EnsureTestClientExists();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar datos: {ex.Message}");
            }
        }

        private async Task EnsureRolesCreated()
        {
            string[] roleNames = { "Admin", "Cliente", "Vendedor" };

            foreach (var roleName in roleNames)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                    Console.WriteLine($"Rol '{roleName}' creado exitosamente.");
                }
            }
        }

        private async Task EnsureAdminUserExists()
        {
            var adminEmail = "admin@golazo.com";
            var adminPassword = "AdminPassword123!";

            var adminUser = await _userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    NombreCompleto = "Administrador Golazo",
                    FechaRegistro = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine($"Usuario admin '{adminEmail}' creado exitosamente.");
                }
                else
                {
                    throw new Exception($"Error al crear usuario admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        private async Task EnsureTestClientExists()
        {
            var clientEmail = "cliente@golazo.com";
            var clientPassword = "ClientPassword123!";

            var clientUser = await _userManager.FindByEmailAsync(clientEmail);

            if (clientUser == null)
            {
                clientUser = new ApplicationUser
                {
                    UserName = clientEmail,
                    Email = clientEmail,
                    EmailConfirmed = true,
                    NombreCompleto = "Cliente de Prueba",
                    FechaRegistro = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(clientUser, clientPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(clientUser, "Cliente");
                    Console.WriteLine($"Usuario cliente '{clientEmail}' creado exitosamente.");
                }
                else
                {
                    Console.WriteLine($"Error al crear usuario cliente: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}