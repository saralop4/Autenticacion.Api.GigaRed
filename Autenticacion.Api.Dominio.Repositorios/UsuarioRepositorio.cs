using Autenticacion.Api.Dominio.DTOs.UsuarioDTOS;
using Autenticacion.Api.Dominio.Interfaces;
using Autenticacion.Api.Dominio.Persistencia;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;

namespace Autenticacion.Api.Infraestructura.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DapperContext _context;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _context = new DapperContext(configuration);
        }

        public Task<bool> Actualizar(UsuarioDto Modelo)
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

        public async Task<bool> ExisteIdPersona(long Id)
        {
            using (var conexion = _context.CreateConnection())
            {
                var query = "SELECT COUNT(1) FROM Usuarios WHERE IdPersona = @IdPersona";
                var existe = await conexion.ExecuteScalarAsync<int>(query, new { IdPersona = Id });

                return existe > 0;

            }
        }

        public async Task<UsuarioDto> Guardar(UsuarioDto Modelo)
        {
            var contraseñaEncriptada = BCrypt.Net.BCrypt.HashPassword(Modelo.Contraseña);

            using (var conexion = _context.CreateConnection()) 
            {

                var query = "RegistrarUsuario";  //nombre del procedimiento almacenado
                var parameters = new DynamicParameters(); 

                parameters.Add("IdRol", Modelo.IdRol);
                parameters.Add("IdPersona", Modelo.IdPersona);
                parameters.Add("Correo", Modelo.Correo);
                parameters.Add("Contraseña", contraseñaEncriptada);
                parameters.Add("UsuarioQueRegistra", Modelo.UsuarioQueRegistra);
                parameters.Add("IpDeRegistro", Modelo.IpDeRegistro);

                var usuarioRegistrado = await conexion.QuerySingleOrDefaultAsync<UsuarioDto>(query, param: parameters, commandType: CommandType.StoredProcedure);

                Console.WriteLine("Valor Devuelto**"+JsonConvert.SerializeObject(usuarioRegistrado));

                return usuarioRegistrado;

            }

        }

        public async Task<UsuarioDto> Obtener(string Id)
        {
            using (var conexion = _context.CreateConnection()) 
            {

                var query = "ObtenerUsuarioPorCorreo"; 
                var parameters = new DynamicParameters(); 
                parameters.Add("Correo", Id);


                var Usuario = await conexion.QuerySingleOrDefaultAsync<UsuarioDto>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return Usuario;
            }
        }

        public Task<IEnumerable<UsuarioDto>> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioDto>> ObtenerTodoConPaginacion(int NumeroDePagina, int TamañoPagina)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioDto> UsuarioAutenticado(IniciarSesionDto IniciarSesionDto)
        {

            if (IniciarSesionDto == null)
                throw new ArgumentNullException(nameof(IniciarSesionDto));
            try
            {

                using (var conexion = _context.CreateConnection())
                {
                    var query = "ObtenerUsuarioAutenticado";
                    var parameters = new DynamicParameters();
                    parameters.Add("Correo", IniciarSesionDto.Correo);

                    var usuario = await conexion.QuerySingleOrDefaultAsync<UsuarioDto>(
                        query,
                        param: parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    // Verificar si el usuario fue encontrado y si la contraseña es válida
                    if (usuario != null && BCrypt.Net.BCrypt.Verify(IniciarSesionDto.Contraseña, usuario.Contraseña))
                    {
                        return usuario;
                    }

                         return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error durante la autenticación del usuario.", ex);
            }
        }

      
    }
}
