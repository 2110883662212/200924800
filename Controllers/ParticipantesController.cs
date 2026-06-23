using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegistroEventos.Models;
using RegistroEventos.Services;

namespace RegistroEventos.Controllers
{
    public class ParticipantesController : Controller    {
        private readonly IEventoService _eventoService;

        // Constructor corregido (sin letras mochadas)
        public ParticipantesController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        // GET: /Participantes
        public IActionResult Index()
        {
            var participantes = _eventoService.ObtenerParticipantes();
            ViewBag.Talleres = _eventoService.ObtenerTalleres();
            return View(participantes);
        }

        // GET: /Participantes/Inscribir
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
            var taller = _eventoService.ObtenerTallerPorId(model.Participante.TallerId);
            if (taller != null && taller.CuposDisponibles <= 0)
            {
                ModelState.AddModelError("Participante.TallerId", $"Lo sentimos, el taller '{taller.Nombre}' ya no tiene cupos disponibles.");
            }

            if (!ModelState.IsValid)
            {
                model.TalleresDisponibles = ObtenerSelectTalleres();
                return View(model);
            }

            bool exito = _eventoService.RegistrarParticipante(model.Participante);
            if (!exito)
            {
                ModelState.AddModelError("", "Ocurrió un error al procesar la inscripción.");
                model.TalleresDisponibles = ObtenerSelectTalleres();
                return View(model);
            }

            TempData["Notificacion"] = $"¡Inscripción Exitosa! {model.Participante.Nombre} ha sido registrado en {taller?.Nombre}.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Participantes/Confirmacion/5
        public IActionResult Confirmacion(int id)
        {
            var participante = _eventoService.ObtenerParticipantePorId(id);
            if (participante == null) return NotFound();

            var taller = _eventoService.ObtenerTallerPorId(participante.TallerId);
            ViewBag.NombreTaller = taller?.Nombre ?? "No asignado";

            return View(participante);
        }

        // Métodos auxiliares privados
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
}