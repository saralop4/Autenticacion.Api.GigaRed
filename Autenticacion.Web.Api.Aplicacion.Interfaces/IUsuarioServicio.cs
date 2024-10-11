using Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;
using Autenticacion.Web.Api.Transversal.Modelos;

namespace Autenticacion.Web.Api.Aplicacion.Interfaces;

public interface IUsuarioServicio
{
    #region Metodos Asincronos

    Task<Response<TokenDto>> AutenticarUsuario(IniciarSesionDto iniciarSesionDto);
    Task<Response<bool>> RegistrarUsuario(UsuarioPersonaDto usuarioDto);
    Task<Response<bool>> ActualizarUsuario(UsuarioPersonaDto usuarioDto);
    Task<Response<bool>> DeleteUsuario(long idUsuario);
    #endregion
}
