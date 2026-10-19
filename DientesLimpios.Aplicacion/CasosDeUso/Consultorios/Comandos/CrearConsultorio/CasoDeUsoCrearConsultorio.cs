using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class CasoDeUsoCrearConsultorio
    {
        private readonly IRepositorioConsultorios _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IValidator<ComandoCrearConsultorio> _validador;

        public CasoDeUsoCrearConsultorio(IRepositorioConsultorios repositorio, IUnidadDeTrabajo unidadDeTrabajo,
            IValidator<ComandoCrearConsultorio> validador)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
            _validador = validador;
        }

        public async Task<Guid> Handle(ComandoCrearConsultorio comando)
        {
            var resultadoValidacion = await _validador.ValidateAsync(comando);
            if (!resultadoValidacion.IsValid)    
            {
                //throw new ArgumentNullException(nameof(resultadoValidacion), "El resultado de la validación no puede ser nulo.");
                throw new ExcepcionDeValidacion(resultadoValidacion);  
            }   

            var consultorio = new Consultorio(comando.Nombre);
            try
            {
                var respuesta = await _repositorio.AgregarAsync(consultorio);
                await _unidadDeTrabajo.Persistir();
                return respuesta.Id;
            }
            catch (Exception)
            {
                await _unidadDeTrabajo.Reservar();
                throw;
            }

        }
    }
}
