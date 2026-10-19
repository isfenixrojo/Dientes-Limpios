namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    public interface IRepositorio<T> where T: class 
    {
        Task<T?> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T> AgregarAsync(T entidad);
        Task ActualizarAsync(T entidad);
        Task BorrarAsync(T entidad);

    }
}
