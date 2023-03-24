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
        [HttpGet]
        public ActionResult Form(ML.Usuario usuario)
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
                if (usuario.Password == usuarioUnbox.Password)
                {
                    return RedirectToAction("Form", "Usuario");
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
    }
}
