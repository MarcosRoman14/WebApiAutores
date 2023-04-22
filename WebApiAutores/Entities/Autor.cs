using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutores.Validations;

namespace WebApiAutores.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, MinimumLength = 3, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres ni menor a {2}")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
