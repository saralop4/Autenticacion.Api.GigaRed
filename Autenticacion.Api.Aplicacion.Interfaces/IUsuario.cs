using Autenticacion.Api.Dominio.DTOs;
using Autenticacion.Api.Transversal.Modelos;

namespace Autenticacion.Api.Aplicacion.Interfaces
{
    public interface IUsuario
    {
        #region Metodos Asincronos

        Task<Response<TokenDto>> AutenticarUsuario(IniciarSesionDto IniciarSesionDto);
        Task<Response<bool>> GuardarUsuario(UsuarioDto UsuarioDto);
        Task<Response<bool>> ActualizarUsuario(UsuarioDto UsuarioDto);
        Task<Response<bool>> DeleteUsuario(string UsuarioId);
        Task<Response<UsuarioDto>> ObtenerUsuario(string UsuarioId);
        Task<Response<IEnumerable<UsuarioDto>>> ObtenerTodosLosUsuarios();
        Task<ResponsePagination<IEnumerable<UsuarioDto>>> ObtenerTodoConPaginación(int NumeroDePagina, int TamañoDePagina);

        #endregion
    }
}
