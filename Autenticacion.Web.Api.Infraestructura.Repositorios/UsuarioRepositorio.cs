using Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;
using Autenticacion.Web.Api.Dominio.Interfaces;
using Autenticacion.Web.Api.Dominio.Persistencia;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Autenticacion.Web.Api.Infraestructura.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DapperContext _context;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _context = new DapperContext(configuration);
        }

        public async Task<bool> Guardar(UsuarioPersonaDto Modelo)
        {
            var contraseñaEncriptada = BCrypt.Net.BCrypt.HashPassword(Modelo.Contraseña);

            using (var conexion = _context.CreateConnection())
            {

                var query = "RegistrarUsuarioYPersona";  
                var parameters = new DynamicParameters();

                parameters.Add("IdIndicativo", Modelo.IdIndicativo);
                parameters.Add("IdCiudad", Modelo.IdCiudad);
                parameters.Add("PrimerNombre", Modelo.PrimerNombre);
                parameters.Add("SegundoNombre", Modelo.SegundoNombre);
                parameters.Add("PrimerApellido", Modelo.PrimerApellido);
                parameters.Add("SegundoApellido", Modelo.SegundoApellido);
                parameters.Add("Telefono", Modelo.Telefono);
                parameters.Add("NombreFoto", Modelo.NombreFoto);
                parameters.Add("UsuarioQueRegistraPersona", Modelo.UsuarioQueRegistraPersona);
                parameters.Add("IpDeRegistroPersona", Modelo.IpDeRegistroPersona);

                parameters.Add("IdRol", Modelo.IdRol);
                parameters.Add("Correo", Modelo.Correo);
                parameters.Add("Contraseña", contraseñaEncriptada);
                parameters.Add("UsuarioQueRegistraUsuario", Modelo.UsuarioQueRegistraUsuario);
                parameters.Add("IpDeRegistroUsuario", Modelo.IpDeRegistroUsuario);

                var usuarioRegistrado = await conexion.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

                return usuarioRegistrado > 0;

            }

        }

        public async Task<UsuarioDto> Obtener(string Id)
        {
            using (var conexion = _context.CreateConnection())
            {

                var query = "ObtenerUsuario";
                var parameters = new DynamicParameters();
                parameters.Add("Correo", Id);
                var Usuario = await conexion.QuerySingleOrDefaultAsync<UsuarioDto>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return Usuario;
            }
        }

        public async Task<UsuarioDto> ValidarUsuario(IniciarSesionDto IniciarSesionDto)
        {

            if (IniciarSesionDto == null)
                throw new ArgumentNullException(nameof(IniciarSesionDto));
            try
            {

                using (var conexion = _context.CreateConnection())
                {
                    var query = "ObtenerUsuario";
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
