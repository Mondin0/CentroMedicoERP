using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using GestionCentroMedico.Models;

namespace GestionCentroMedico.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ingresar(Usuario usuarioDTO)
        {
            Usuario user = new Usuario
            {
                codigo = "sa",
                password = "1234"
            };

            if (usuarioDTO.codigo == user.codigo && usuarioDTO.password==user.password )
            {
                var claims = new List<Claim>
                {
                    new Claim("codigo", usuarioDTO.codigo.ToString()),
                    new Claim("password", usuarioDTO.password.ToString()),
                    //new Claim("Clave",clave)

                };
                //foreach (var rol in usuarioDTO.rol)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, rol.ToString()));
                //}
                //claims.Add(new Claim(ClaimTypes.Role, usuarioDTO.rol));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                usuarioDTO.autenticado = true;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Error de inicio de sesión";
                return View();
            }

        }
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
