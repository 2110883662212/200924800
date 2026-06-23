namespace RegistroEventos.Models
{
    public class Taller    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int CupoMaximo { get; set; }
        public int CuposRegistrados { get; set; }
        public int CuposDisponibles => CupoMaximo - CuposRegistrados;
    }
}