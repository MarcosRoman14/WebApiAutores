using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Validations
{
    public class PrimeraLetraMayusculaAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string primeraLetra = value.ToString()[0].ToString();

            if(primeraLetra != primeraLetra.ToUpper()) return new ValidationResult("La primer letra debe ser mayuscula");

            return ValidationResult.Success;
        }
    }
}
