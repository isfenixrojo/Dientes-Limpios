using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;



namespace DientesLimpios.Prueba.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoCrearConsultorioTest
    {
        private IRepositorioConsultorios _repositorio;
        private IValidator<ComandoCrearConsultorio> _validador;
        private IUnidadDeTrabajo _unidadDeTrabajo;
        private CasoDeUsoCrearConsultorio _casoDeUso;

        [TestInitialize]
        public void SetUp()
        {
            _repositorio = Substitute.For<IRepositorioConsultorios>();
            _validador = Substitute.For<IValidator<ComandoCrearConsultorio>>();
            _unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            _casoDeUso = new CasoDeUsoCrearConsultorio(_repositorio, _unidadDeTrabajo, _validador);
        }

        [TestMethod]
        public async Task Handle_ComandoValido_ObtenerIdConsultorio()
        {
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio Test" };
            _validador.ValidateAsync(comando).Returns(new ValidationResult());

            var consultorioCreado = new Consultorio("Consultorio Test");
            _repositorio.AgregarAsync(Arg.Any<Consultorio>()).Returns(consultorioCreado);

            var resultado = await _casoDeUso.Handle(comando);

            await _validador.Received(1).ValidateAsync(comando);
            await _repositorio.Received(1).AgregarAsync(Arg.Any<Consultorio>());
            await _unidadDeTrabajo.Received(1).Persistir();
            Assert.AreNotEqual(Guid.Empty, resultado);

        }

        [TestMethod]
        public async Task Handle_ComandoNoInvalido_LanzaExcepcion()
        {
            var comando = new ComandoCrearConsultorio { Nombre = "" };
            var resultadoValidacion = new ValidationResult(new[]
            {
                new ValidationFailure("Nombre", "El nombre es obligatorio.")
            });

            _validador.ValidateAsync(comando).Returns(resultadoValidacion);

            await Assert.ThrowsExceptionAsync<ExcepcionDeValidacion>(async () =>
            {
                await _casoDeUso.Handle(comando);
            });

            await _repositorio.DidNotReceive().AgregarAsync(Arg.Any<Consultorio>());
        }
        [TestMethod]
        public async Task Handle_CuandoHayError_HacemosRollback()
        {
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio Test" };
            _repositorio.AgregarAsync(Arg.Any<Consultorio>()).Throws<Exception>();
            _validador.ValidateAsync(comando).Returns(new ValidationResult());
            
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                var resultado = await _casoDeUso.Handle(comando);
            });
            await _unidadDeTrabajo.Received(1).Reservar();
        }

    }
}
