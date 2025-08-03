using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Proyecto_Final.Models.Usuario;
using System.Threading.Tasks;

namespace Proyecto_Final.Services
{
    public class AuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly NavigationManager _navigationManager;

        public AuthService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            NavigationManager navigationManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _navigationManager = navigationManager;
        }

        public async Task<SignInResult> Login(string email, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(
                email, password, rememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _navigationManager.NavigateTo("/perfil", forceLoad: true);
            }

            return result;
        }

        public async Task<IdentityResult> Register(string email, string password, string fullName)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                NombreCompleto = fullName,
                EmailConfirmed = true // Para desarrollo
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Cliente");
                await _signInManager.SignInAsync(user, isPersistent: false);
                _navigationManager.NavigateTo("/perfil", forceLoad: true);
            }

            return result;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            _navigationManager.NavigateTo("/");
        }

        public async Task<bool> IsUserInRole(ApplicationUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<IList<string>> GetUserRoles(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}