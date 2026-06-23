using Microsoft.AspNetCore.Mvc.Rendering;

namespace RegistroEventos.Models
{
    public class InscripcionViewModel    {
        public Participante Participante { get; set; } = new();
        public List<SelectListItem> TalleresDisponibles { get; set; } = new();
    }
}