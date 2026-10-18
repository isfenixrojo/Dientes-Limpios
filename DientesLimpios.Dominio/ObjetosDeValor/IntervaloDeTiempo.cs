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
                throw new ExcepcionReglaDeNegocio("La fecha de inicio debe ser menor a la fecha de fin");
            }

            /*if (fechaInicio < DateTime.Now)
            {
                throw new ExcepcionReglaDeNegocio("La fecha de inicio debe ser mayor a la fecha actual");
            }*/
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}
