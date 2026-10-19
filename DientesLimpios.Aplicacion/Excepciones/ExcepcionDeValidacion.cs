using FluentValidation.Results;

namespace DientesLimpios.Aplicacion.Excepciones
{
    public class ExcepcionDeValidacion : Exception
    {
        public List<string> ErroresDeValidacion { get; set; } = [];
        public ExcepcionDeValidacion(ValidationResult validationResult)
        {
            foreach (var erroresDeValidacion in validationResult.Errors)
            {
                ErroresDeValidacion.Add(erroresDeValidacion.ErrorMessage);
            }
        }
    }
}