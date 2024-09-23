using Autenticacion.Api.Dominio.DTOs;
using Autenticacion.Api.Infraestructura.Interfaces;
using Dapper;
using Ecommerce.Infraestructura.Data;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Autenticacion.Api.Dominio.Repositorios
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

        public Task<bool> Eliminar(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Guardar(UsuarioDto Modelo)
        {
            var contraseñaEncriptada = BCrypt.Net.BCrypt.HashPassword(Modelo.Contraseña);

            using (var connection = _context.CreateConnection()) //el metodo Get devuelve la instancia de conexion abierta
            {

                var query = "GuardarUsuario"; //nombre del procedimiento almacenado
                var parameters = new DynamicParameters(); //para el uso de dapper se recomienda el uso de parametros dinamicos
                                                          //parameters es una lista de parametros
                parameters.Add("IdRol", Modelo.IdRol);
                parameters.Add("IdPersona", Modelo.IdPersona);
                parameters.Add("Correo", Modelo.Correo);
                parameters.Add("Contraseña", contraseñaEncriptada);
                parameters.Add("IpRegistro", Modelo.IpDeRegistro);

                var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                //el metodo execute permite invocar un procedimiento almacenado y enviarle los parametros
                return result > 0;

            }

        }

        public Task<UsuarioDto> Obtener(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioDto>> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioDto>> ObtenerTodoConPaginacion(int NumeroDePagina, int TamañoPagina)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioDto> UsuarioAutenticado(IniciarSesionDto iniciarSesionDto)
        {
            if (iniciarSesionDto == null)
                throw new ArgumentNullException(nameof(iniciarSesionDto));

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "ObtenerUsuarioAutenticado";
                    var parameters = new DynamicParameters();
                    parameters.Add("Correo", iniciarSesionDto.Correo);
                    parameters.Add("Contraseña", iniciarSesionDto.Contraseña);

                    var usuario = await connection.QuerySingleOrDefaultAsync<UsuarioDto>(
                        query,
                        param: parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    // Verificar si el usuario fue encontrado y si la contraseña es válida
                    if (usuario != null && BCrypt.Net.BCrypt.Verify(iniciarSesionDto.Contraseña, usuario.Contraseña))
                    {
                        return usuario;
                    }

                    // Si la autenticación falla, devuelve null o lanza una excepción según tu lógica
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error durante la autenticación del usuario.", ex);
            }
        }

        private string GetMd5Hash(string Dato)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(Dato));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}
