using Autenticacion.Web.Api.Dominio.DTOs.PersonaDTOs;
using Autenticacion.Web.Api.Transversal.Modelos;

namespace Autenticacion.Web.Api.Aplicacion.Interfaces;

public interface IPersonaServicio
{
    #region Metodos Asincronos

    Task<Response<PersonaDto>> RegistrarPersona(PersonaDto PersonaDto);
    Task<Response<bool>> ActualizarUsuario(PersonaDto PersonaDto);
    Task<Response<bool>> DeleteUsuario(long IdPersona);
    Task<Response<PersonaDto>> ObtenerUsuario(string IdPersona);
    Task<Response<IEnumerable<PersonaDto>>> ObtenerTodosLasPersonas();
    Task<ResponsePagination<IEnumerable<PersonaDto>>> ObtenerTodoConPaginación(int NumeroDePagina, int TamañoDePagina);

    #endregion
}
