using System.ComponentModel.DataAnnotations;

namespace RegistroEventos.Models
{
    public class Participante    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un taller.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un taller válido.")]
        public int TallerId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el tipo de entrada.")]
        public string TipoEntrada { get; set; } = string.Empty;

        // Modificado para evitar el bug del Range booleano en cliente
        [Display(Name = "Acepto los términos y condiciones")]
        public bool AceptaTerminos { get; set; }
    }
}
