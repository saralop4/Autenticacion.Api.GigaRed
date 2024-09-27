using Autenticacion.Api.Aplicacion.Interfaces;
using Autenticacion.Api.Aplicacion.Validadores;
using Autenticacion.Api.Dominio.DTOs.PersonaDTOS;
using Autenticacion.Api.Dominio.Interfaces;
using Autenticacion.Api.Transversal.Modelos;

namespace Autenticacion.Api.Aplicacion.Servicios
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
                response.Message = "Errores de validacion";
                response.Errors = validation.Errors;
                return response;

            }

            var Persona = await _PersonaRepositorio.Guardar(PersonaDto);

                if (Persona is {}) // no es nulo
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
