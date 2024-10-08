using Autenticacion.Web.Api.Aplicacion.Interfaces;
using Autenticacion.Web.Api.Transversal.Modelos;
using Autenticacion.Web.Api.Aplicacion.Validadores;
using Autenticacion.Web.Api.Dominio.DTOs.PersonaDTOs;
using Autenticacion.Web.Api.Dominio.Interfaces;

namespace Autenticacion.Web.Api.Aplicacion.Servicios
{
    public class PersonaServicio : IPersonaServicio
    {
        private readonly IPersonaRepositorio _PersonaRepositorio;
        private readonly PersonaDtoValidador _PersonaDtoValidador;
        public PersonaServicio(IPersonaRepositorio PersonaRepositorio, PersonaDtoValidador PersonaDtoValidador)
        {
            _PersonaRepositorio = PersonaRepositorio;
            _PersonaDtoValidador = PersonaDtoValidador;
        }

        public Task<Response<bool>> ActualizarUsuario(PersonaDto PersonaDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> DeleteUsuario(long IdPersona)
        {
            throw new NotImplementedException();
        }

        public Task<ResponsePagination<IEnumerable<PersonaDto>>> ObtenerTodoConPaginación(int NumeroDePagina, int TamañoDePagina)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<PersonaDto>>> ObtenerTodosLasPersonas()
        {
            throw new NotImplementedException();
        }

        public Task<Response<PersonaDto>> ObtenerUsuario(string IdPersona)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<PersonaDto>> RegistrarPersona(PersonaDto PersonaDto)
        {
            var response = new Response<PersonaDto>();
            var validation = _PersonaDtoValidador.Validate(new PersonaDto()
            {
                PrimerNombre = PersonaDto.PrimerNombre,
                PrimerApellido = PersonaDto.PrimerApellido,
                Telefono = PersonaDto.Telefono,
                UsuarioQueRegistra = PersonaDto.UsuarioQueRegistra
            });

            if (!validation.IsValid)
            {
                response.IsSuccess = false;
                response.Message = "Errores de validación encontrados";
                response.Errors = validation.Errors;
                return response;

            }

            var Persona = await _PersonaRepositorio.Guardar(PersonaDto);

            if (Persona is { }) // no es nulo
            {
                response.Data = Persona;
                response.IsSuccess = true;
                response.Message = "Registro exitoso!!";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Hubo error al crear el registro";
            }
            return response;
        }
    }
}
