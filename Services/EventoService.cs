using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RegistroEventos.Models;

namespace RegistroEventos.Controllers
{
    public class HomeController : Controller    {
        public IActionResult Index()
        {
            // Redirigimos automáticamente al controlador de participantes
            return RedirectToAction("Index", "Participantes");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
