using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult LoginUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUsuario(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetByUserName(usuario);

            if (result.Correct)
            {
                ML.Usuario usuarioUnbox = (ML.Usuario)result.Object;
                //SESION 
                int IdUsuario = usuarioUnbox.IdUsuario;
                HttpContext.Session.SetInt32("IdUsuario",IdUsuario);
                usuarioUnbox.IdUsuario= IdUsuario;

                if (usuario.Password == usuarioUnbox.Password)
                {
                    HttpContext.Session.GetInt32("IdUsuario");
                    return RedirectToAction("Form", "SuperDigito");
                }
                else
                {
                    ViewBag.Message = "La contraseña es incorrecta";
                    return PartialView("Modal");
                    
                }

            }
            else
            {
                ViewBag.Message = "El Nombre de Usuario es incorrecta o no existe";
                return PartialView("Modal");
            }

        }
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            return View(usuario);
        }
    }
}
