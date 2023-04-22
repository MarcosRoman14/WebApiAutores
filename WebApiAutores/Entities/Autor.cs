using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutores.Validations;

namespace WebApiAutores.Entities
{
    public class Autor : IValidatableObject
    {
        public int Id { get; set; }
        [DisplayName("Nombresiño")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, MinimumLength = 3, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres ni menor a {2}")]
        //[PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        //[Range(18, 120)]
        //[NotMapped] // No relacionada a la bd
        //public int Edad { get; set; }
        //[CreditCard]
        //[NotMapped] // No relacionada a la bd
        //public string TarjetaDeCredito { get; set; }

        //[Url]
        //[NotMapped] // No relacionada a la bd
        ////public string Url { get; set; }
        public List<Libro> Libros { get; set; }

        //public int Menor { get; set; }
        //public int Mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula",
                        new string[] { nameof(Nombre) });
                }
            }
            //if (Menor > Mayor)
            //{
            //    yield return new ValidationResult("Este valor no puede ser más grande que el campo Mayor",
            //        new string[] { nameof(Menor) });
            //}
        }
    }
}
