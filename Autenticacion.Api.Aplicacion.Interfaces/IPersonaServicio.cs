using Autenticacion.Api.Dominio.DTOs.PersonaDTOS;
using Autenticacion.Api.Dominio.DTOs.UsuarioDTOS;
using Autenticacion.Api.Transversal.Modelos;

namespace Autenticacion.Api.Aplicacion.Interfaces
{
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
}
