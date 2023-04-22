using WebApiAutores.Validations;

namespace WebApiAutores.Entities
{
    public class Libro
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string Titulo { get; set; }
    }
}
