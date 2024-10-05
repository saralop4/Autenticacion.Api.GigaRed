using Autenticacion.Api.Dominio.DTOs.PersonaDTOS;
using Autenticacion.Api.Dominio.Interfaces;
using Autenticacion.Api.Dominio.Persistencia;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Autenticacion.Api.Infraestructura.Repositorios
{
    public class PersonaRepositorio : IPersonaRepositorio
    {
        private readonly DapperContext _context;

        public PersonaRepositorio(IConfiguration configuration)
        {
            _context = new DapperContext(configuration);

        }
        public Task<bool> Actualizar(PersonaDto Modelo)
        {
            throw new NotImplementedException();
        }

        public Task<int> Contar()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonaDto> Guardar(PersonaDto Modelo)
        {
            using (var conexion = _context.CreateConnection())
            {

                var query = "GuardarPersona";  
                var parameters = new DynamicParameters();

                parameters.Add("IdIndicativo", Modelo.IdIndicativo);
                parameters.Add("PrimerNombre", Modelo.PrimerNombre);
                parameters.Add("PrimerApellido", Modelo.PrimerApellido);
                parameters.Add("Telefono", Modelo.Telefono);
                parameters.Add("UsuarioQueRegistra", Modelo.UsuarioQueRegistra);
                parameters.Add("IpDeRegistro", Modelo.IpDeRegistro);

                var Resultado = await conexion.QuerySingleOrDefaultAsync<PersonaDto>(query, param: parameters, commandType: CommandType.StoredProcedure);


                return Resultado;

            }
        }

        public async Task<PersonaDto> Obtener(string Id)
        {
            using (var Conexion = _context.CreateConnection())
            {
                var Query = "ObtenerPersona";
                var Parameters = new DynamicParameters();
                Parameters.Add("IdPersona", Id);

                var Persona = await Conexion.QuerySingleOrDefaultAsync<PersonaDto>(Query, param: Parameters, commandType: CommandType.StoredProcedure);

                return Persona;
            }           
        }

        public Task<IEnumerable<PersonaDto>> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonaDto>> ObtenerTodoConPaginacion(int NumeroDePagina, int TamañoPagina)
        {
            throw new NotImplementedException();
        }
    }
}
