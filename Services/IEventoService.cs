using RegistroEventos.Models;

namespace RegistroEventos.Services
{
    public interface IEventoService    {
        List<Taller> ObtenerTalleres();
        List<Participante> ObtenerParticipantes();
        Participante? ObtenerParticipantePorId(int id);
        Taller? ObtenerTallerPorId(int id);
        bool RegistrarParticipante(Participante participante);
    }
}