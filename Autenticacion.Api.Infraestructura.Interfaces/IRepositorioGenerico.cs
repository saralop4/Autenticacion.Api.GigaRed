namespace Autenticacion.Api.Infraestructura.Interfaces
{
    public interface IRepositorioGenerico<T> where T : class //agregamos una restriccion para que T siempre sea de tipo class
    {
        #region Metodos Asincronos
        Task<bool> Guardar(T Modelo);
        Task<bool> Actualizar(T Modelo);
        Task<bool> Eliminar(long Id);
        Task<T> Obtener(string Id);
        Task<IEnumerable<T>> ObtenerTodo();
        Task<IEnumerable<T>> ObtenerTodoConPaginacion(int NumeroDePagina, int TamañoPagina);
        Task<int> Contar();

        #endregion
    }
}
