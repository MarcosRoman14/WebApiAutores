using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validations;

namespace WebApiAutores.DTOs
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, MinimumLength = 3, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres ni menor a {2}")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
