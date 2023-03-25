using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class SuperDigitoController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public SuperDigitoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult Form()
        {
            ML.Historial historial = new ML.Historial();
            int? IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ML.Result result = BL.Historial.HistorialGetByIdUsuario(IdUsuario);

            historial.Historiales = result.Objects;
            return View(historial);

        }

        [HttpPost]
        public ActionResult Form(ML.Historial historial)
        {
            ML.Result result = new ML.Result();
            int? IdUsuario = HttpContext.Session.GetInt32("IdUsuario");

            historial.Usuario = new ML.Usuario();
            historial.Usuario.IdUsuario = IdUsuario.Value;
            historial.Resultado = BL.Historial.CalcularSuperDigito(historial.Numero.ToString());

            ML.Result resultAdd = BL.Historial.Add(historial);
            result = BL.Historial.HistorialGetByIdUsuario(IdUsuario.Value);

            if (result.Correct && resultAdd.Correct)
            {
                historial.Historiales = result.Objects;
                return View(historial);
            }
            else
            {
                return View(historial);
            }

        }

        [HttpGet]

        public ActionResult Delete(int idHistorial)
        {
            ML.Result resultDelete = new ML.Result();
            resultDelete = BL.Historial.Delete(idHistorial);

            if (resultDelete.Correct)
            {
                ViewBag.Message = "EL REGISTRO SE ELIMINO CORRECTAMENTE";
                return PartialView("Modal");

            }
            else
            {
                ViewBag.Message = "EL REGISTRO NO SE ELIMINO CORRECTAMENTE";
                return PartialView("Modal");
            }
        }

    }
}
