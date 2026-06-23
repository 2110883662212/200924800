using RegistroEventos.Models;

namespace RegistroEventos.Services
{
    public class EventoService : IEventoService    {
        // Listas estáticas simulando la base de datos
        private static readonly List<Taller> _talleres = new()
        {
            new Taller { Id = 1, Nombre = "Taller de Clean Architecture en .NET", CupoMaximo = 3, CuposRegistrados = 0 },
            new Taller { Id = 2, Nombre = "Conferencia: El futuro de la IA con C#", CupoMaximo = 2, CuposRegistrados = 0 },
            new Taller { Id = 3, Nombre = "Workshop de Docker para Desarrolladores", CupoMaximo = 15, CuposRegistrados = 0 }
        };

        private static readonly List<Participante> _participantes = new();
        private static int _nextParticipanteId = 1;

        public List<Taller> ObtenerTalleres() => _talleres;
        
        public List<Participante> ObtenerParticipantes() => _participantes;
        
        public Participante? ObtenerParticipantePorId(int id) => 
            _participantes.FirstOrDefault(p => p.Id == id);

        public Taller? ObtenerTallerPorId(int id) => 
            _talleres.FirstOrDefault(t => t.Id == id);

        public bool RegistrarParticipante(Participante participante)
        {
            var taller = ObtenerTallerPorId(participante.TallerId);
            if (taller == null || taller.CuposDisponibles <= 0)
            {
                return false;
            }

            participante.Id = _nextParticipanteId++;
            taller.CuposRegistrados++;
            _participantes.Add(participante);
            return true;
        }
    }
}