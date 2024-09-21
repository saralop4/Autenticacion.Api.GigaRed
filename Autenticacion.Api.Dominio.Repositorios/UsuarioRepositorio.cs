using Autenticacion.Api.Dominio.DTOs;
using Autenticacion.Api.Dominio.Persistencia.DbContextMigraciones;
using Autenticacion.Api.Infraestructura.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Autenticacion.Api.Dominio.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IConfiguration _configuration;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
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

        public Task<bool> Guardar(UsuarioDto Modelo)
        {
            throw new NotImplementedException();
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

        public async Task<UsuarioDto> UsuarioAutenticado(IniciarSesionDto IniciarSesionDto)
        {
            // Obtener la cadena de conexión según el valor de dto.Conexion
            string CadenaConexion = ObtenerCadenaDeConexion(IniciarSesionDto.Conexion);

            if (CadenaConexion == null)
            {
                // La cadena de conexión no se encontró, la autenticación no es posible
                return null;
            }

            // Crear DbContextOptions usando la cadena de conexión
            var dbContextOptions = new DbContextOptionsBuilder<AutenticacionDbContext>()
                .UseSqlServer(CadenaConexion) // Reemplaza UseSqlServer con el proveedor de base de datos correcto
                .Options;

            using (var conexion = new SqlConnection(CadenaConexion))
            {
                await conexion.OpenAsync(); // Método asíncrono para abrir la conexión

                string Contraseña = GetMd5Hash(IniciarSesionDto.Contraseña);

                var query = "ObtenerUsuarioAutenticado";
                var parameters = new DynamicParameters();
                parameters.Add("Correo", IniciarSesionDto.Correo);
                parameters.Add("Contraseña", Contraseña);

                // Cambia QuerySingle por su versión asíncrona QuerySingleAsync
                var usuario = await conexion.QuerySingleAsync<UsuarioDto>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return usuario;
            }
        }

        private string ObtenerCadenaDeConexion(string NombreConexion)
        {
            var databaseConnections = new Dictionary<string, string>();
            var databaseSection = _configuration.GetSection("ConnectionStrings");

            foreach (var childSection in databaseSection.GetChildren())
            {
                var key = childSection.Key;
                var value = childSection.Value;

                databaseConnections[key] = value;
            }

            if (databaseConnections.ContainsKey(NombreConexion))
            {
                return databaseConnections[NombreConexion];
            }

            return null; // Devolver nulo si no se encontró la conexión
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
