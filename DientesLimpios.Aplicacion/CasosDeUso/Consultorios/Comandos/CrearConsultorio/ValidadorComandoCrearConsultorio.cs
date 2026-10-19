using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class ValidadorComandoCrearConsultorio : AbstractValidator<ComandoCrearConsultorio>
    {
        public ValidadorComandoCrearConsultorio()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del {PropertyName} no puede estar vacío.");
                //.MaximumLength(100).WithMessage("El nombre del consultorio no puede exceder los 100 caracteres.");  
        }

    }
}
