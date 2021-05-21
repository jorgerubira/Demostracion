using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demostracion.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "sistemas")]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario, string password)
        {
            //Consultar en la base de datos. Revisar el password
            //Pendiente

            List<Claim> infor = new List<Claim>();
            infor.Add(new Claim(ClaimTypes.Name, usuario));
            infor.Add(new Claim(ClaimTypes.Email, "a@a.com"));
            infor.Add(new Claim("edad", "22"));
            infor.Add(new Claim(ClaimTypes.Role, "administrativo"));
            infor.Add(new Claim(ClaimTypes.Role, "sistemas"));

            ClaimsIdentity ci = new ClaimsIdentity(infor);
            ClaimsPrincipal cp = new ClaimsPrincipal(new[] { ci });

            HttpContext.SignInAsync(cp);

            return Redirect("/Usuarios/Secret");

        }

        public IActionResult Authorize()
        {
            //Consultar en la base de datos. Revisar el password
            //Pendiente

            List<Claim> infor = new List<Claim>();
            infor.Add(new Claim(ClaimTypes.Name, "usuario"));
            infor.Add(new Claim(ClaimTypes.Email, "a@a.com"));
            infor.Add(new Claim("edad", "22"));
            //infor.Add(new Claim(ClaimTypes.Role, "administrativo"));
            //infor.Add(new Claim(ClaimTypes.Role, "sistemas"));

            ClaimsIdentity ci = new ClaimsIdentity(infor);
            ClaimsPrincipal cp = new ClaimsPrincipal(new[] { ci });

            HttpContext.SignInAsync(cp);

            return Redirect("/Usuarios/Index");

        }


    }
}