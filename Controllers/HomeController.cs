using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegistroEventos.Models;
using RegistroEventos.Services;

namespace RegistroEventos.Controllers
{
    public class HomeController(IEventoService eventoService) : Controller    {
        private readonly IEventoService _eventoService = eventoService;

        // GET: /Participantes (Lista total)
        public IActionResult Index()
        {
            var participantes = _eventoService.ObtenerParticipantes();
            // Necesitamos los talleres para mapear los nombres en la vista si fuera necesario
            ViewBag.Talleres = _eventoService.ObtenerTalleres();
            return View(participantes);
        }

        // GET: /Participantes/Inscribir (Formulario)
        public IActionResult Inscribir()
        {
            var viewModel = CargarInscripcionViewModel();
            return View(viewModel);
        }

        // POST: /Participantes/Inscribir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inscribir(InscripcionViewModel model)
        {
            // Validar cupo en el servicio antes del ModelState formal
            var taller = _eventoService.ObtenerTallerPorId(model.Participante.TallerId);
            if (taller != null && taller.CuposDisponibles <= 0)
            {
                ModelState.AddModelError("Model.Participante.TallerId", $"Lo sentimos, el taller '{taller.Nombre}' ya no tiene cupos disponibles.");
            }

            if (!ModelState.IsValid)
            {
                // Recargar el dropdown en caso de error
                model.TalleresDisponibles = ObtenerSelectTalleres();
                return View(model);
            }

            bool exito = _eventoService.RegistrarParticipante(model.Participante);
            if (!exito)
            {
                ModelState.AddModelError("", "Ocurrió un error al procesar la inscripción. Inténtelo de nuevo.");
                model.TalleresDisponibles = ObtenerSelectTalleres();
                return View(model);
            }

            // Aplicando PRG + TempData
            TempData["Notificacion"] = $"¡Inscripción Exitosa! {model.Participante.Nombre} ha sido registrado en {taller?.Nombre}.";
            
            return RedirectToAction(nameof(Index));
        }

        // GET: /Participantes/Confirmacion/5 (Vista individual estilo PDF)
        public IActionResult Confirmacion(int id)
        {
            var participante = _eventoService.ObtenerParticipantePorId(id);
            if (participante == null) return NotFound();

            var taller = _eventoService.ObtenerTallerPorId(participante.TallerId);
            ViewBag.NombreTaller = taller?.Nombre ?? "No asignado";

            return View(participante);
        }

        // Métodos auxiliares
        private List<SelectListItem> ObtenerSelectTalleres()
        {
            return _eventoService.ObtenerTalleres().Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = $"{t.Nombre} (Cupos: {t.CuposDisponibles}/{t.CupoMaximo})"
            }).ToList();
        }

        private InscripcionViewModel CargarInscripcionViewModel()
        {
            return new InscripcionViewModel
            {
                TalleresDisponibles = ObtenerSelectTalleres()
            };
        }
    }

    public interface IEventoService
    {
    }
}