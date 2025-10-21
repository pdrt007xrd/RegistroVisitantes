using System.ComponentModel.DataAnnotations;

namespace WebApp.RegistroVisitantes.Models
{
    public class Visitante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cédula es obligatoria")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El motivo es obligatorio")]
        public string Motivo { get; set; } = string.Empty;

        public DateTime FechaHora { get; set; }
    }
}
