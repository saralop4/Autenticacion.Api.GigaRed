using Autenticacion.Web.Api.Dominio.DTOs.PersonaDTOs;
using Autenticacion.Web.Api.Dominio.Interfaces;
using Autenticacion.Web.Api.Dominio.Persistencia;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Autenticacion.Web.Api.Infraestructura.Repositorios
{
    public class PersonaRepositorio : IPersonaRepositorio
    {
        private readonly DapperContext _context;

        public PersonaRepositorio(IConfiguration configuration)
        {
            _context = new DapperContext(configuration);

        }
        public async Task<bool> Actualizar(PersonaDto Modelo)
        {
            using (var conexion = _context.CreateConnection())
            {
                var query = "ActualizarPersonas";
                var parameters = new DynamicParameters();
                parameters.Add("IdPersona", Modelo.IdPersona);
                parameters.Add("IdIndicativo", Modelo.IdIndicativo);
                parameters.Add("PrimerNombre", Modelo.PrimerNombre);
                parameters.Add("SegundoNombre", Modelo.SegundoNombre);
                parameters.Add("PrimerApellido", Modelo.PrimerApellido);
                parameters.Add("SegundoApellido", Modelo.SegundoApellido);
                parameters.Add("Telefono", Modelo.Telefono);
                parameters.Add("UsuarioQueActualiza", Modelo.UsuarioQueActualiza);
                parameters.Add("FechaDeActualizado", Modelo.FechaDeActualizado);
                parameters.Add("HoraDeActualizado", Modelo.HoraDeActualizado);
                parameters.Add("IpDeActualizado", Modelo.IpDeActualizado);

                var result = await conexion.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
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

                var query = "RegistrarPersona";
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
