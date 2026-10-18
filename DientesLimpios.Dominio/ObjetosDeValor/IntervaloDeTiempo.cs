using DientesLimpios.Dominio.Excepciones;

namespace DientesLimpios.Dominio.ObjetosDeValor
{
    public class IntervaloDeTiempo
    {
        public DateTime FechaInicio { get; }
        public DateTime FechaFin { get; }
        public IntervaloDeTiempo(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio >= fechaFin)
            {
                throw new ExcepcionReglaDeNegocio("La hora de inicio debe ser anterior a la hora de fin.");
            }

            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}
