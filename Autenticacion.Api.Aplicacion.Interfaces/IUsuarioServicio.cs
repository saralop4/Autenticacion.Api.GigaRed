using Autenticacion.Api.Dominio.DTOs.UsuarioDTOS;
using Autenticacion.Api.Transversal.Modelos;

namespace Autenticacion.Api.Aplicacion.Interfaces
{
    public interface IUsuarioServicio
    {
        #region Metodos Asincronos

        Task<Response<TokenDto>> AutenticarUsuario(IniciarSesionDto IniciarSesionDto);
        Task<Response<bool>> RegistrarUsuario(UsuarioDto UsuarioDto);
        Task<Response<bool>> ActualizarUsuario(UsuarioDto UsuarioDto);
        Task<Response<bool>> DeleteUsuario(long IdUsuario);
        Task<Response<UsuarioDto>> ObtenerUsuario(string Correo);
        Task<Response<IEnumerable<UsuarioDto>>> ObtenerTodosLosUsuarios();
        Task<ResponsePagination<IEnumerable<UsuarioDto>>> ObtenerTodoConPaginación(int NumeroDePagina, int TamañoDePagina);

        #endregion
    }
}
