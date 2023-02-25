using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutores.Validations;

namespace WebApiAutores.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [DisplayName("Nombresiño")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50,MinimumLength = 3, ErrorMessage ="El campo {0} no puede ser mayor a {1} caracteres ni menor a {2}")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        
        [Range(18, 120)]
        [NotMapped] // No relacionada a la bd
        public int Edad { get; set; }
        [CreditCard]
        [NotMapped] // No relacionada a la bd
        public string TarjetaDeCredito { get; set; }

        [Url]
        [NotMapped] // No relacionada a la bd
        public string Url { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
