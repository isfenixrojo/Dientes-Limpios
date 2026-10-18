using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;

namespace DientesLimpios.Dominio.Entidades
{
    public class Cita
    {
        public Guid Id { get; private set; }
        public Guid PacienteId { get; private set; }
        public Guid DentistaId { get; private set; }
        public Guid ConsultorioId { get; private set; }
        public EstadoCita Estado { get; private set; }
        public IntervaloDeTiempo IntervaloDeTiempo { get; private set; }
        public Paciente? Paciente { get; private set; }
        public Dentista? Dentista { get; private set; }
        public Consultorio? Consultorio { get; private set; }

        public Cita(Guid pacienteId, Guid dentistaId, Guid consultorioId, IntervaloDeTiempo intervaloDeTiempo)
        {
            if (intervaloDeTiempo.FechaInicio < DateTime.UtcNow)
            {
                throw new ExcepcionReglaDeNegocio("La fecha de inicio debe ser mayor a la fecha actual");
            }

            PacienteId = pacienteId;
            DentistaId = dentistaId;
            ConsultorioId = consultorioId;
            IntervaloDeTiempo = intervaloDeTiempo;
            Estado = EstadoCita.Programada;
            Id = Guid.CreateVersion7();
        }

        public void Cancelar()
        {
            if (Estado != EstadoCita.Programada)
            {
                throw new ExcepcionReglaDeNegocio("La cita ya se encuentra cancelada");
            }

            Estado = EstadoCita.Cancelada;
        }

        public void Completar()
        {
            if (Estado != EstadoCita.Programada)
            {
                throw new ExcepcionReglaDeNegocio("Solo se pueden completar citas programadas");
            }
            Estado = EstadoCita.Completada;
        }
    }
}
