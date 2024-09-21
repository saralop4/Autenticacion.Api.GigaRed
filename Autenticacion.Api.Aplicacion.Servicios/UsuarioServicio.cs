using Autenticacion.Api.Aplicacion.Interfaces;
using Autenticacion.Api.Aplicacion.Validador;
using Autenticacion.Api.Dominio.DTOs;
using Autenticacion.Api.Infraestructura.Interfaces;
using Autenticacion.Api.Transversal.Modelos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Autenticacion.Api.Aplicacion.Servicios
{
    public class UsuarioServicio : IUsuario
    {
        private readonly IUsuarioRepositorio _UsuarioRepositorio;
        private readonly IniciarSesionDtoValidador _IniciarSesionDtoValidador;
        private readonly double _jwtExpirationMinutes;
        private readonly IConfiguration _configuration;

        public UsuarioServicio(IConfiguration configuracion,IUsuarioRepositorio UsuarioRepositorio, IniciarSesionDtoValidador IniciarSesionDtoValidador)
        {
            _configuration = configuracion;
            _UsuarioRepositorio = UsuarioRepositorio;
            _IniciarSesionDtoValidador = IniciarSesionDtoValidador;
            _jwtExpirationMinutes = 7 * 24 * 60; // 7 días en minutos
        }

        public Task<Response<bool>> ActualizarUsuario(UsuarioDto UsuarioDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<TokenDto>> AutenticarUsuario(IniciarSesionDto IniciarSesionDto)
        {
            var response = new Response<TokenDto>();
            var validation = _IniciarSesionDtoValidador.Validate(new IniciarSesionDto() { Correo = IniciarSesionDto.Correo, Contraseña = IniciarSesionDto.Contraseña }); //esta variable almacena la respuesta del validador fluent

            if (!validation.IsValid)
            {
                response.Message = "Errores de Validacion";
                response.Errors = validation.Errors;
                return response;
            }

            try
            {
                var usuarioValidado = await _UsuarioRepositorio.UsuarioAutenticado(IniciarSesionDto);
        
                if (usuarioValidado is { })
                {
                    string token =  GenerateJwtToken(usuarioValidado.IdUsuario, usuarioValidado.Correo);

                    TokenDto TokenDto = new TokenDto{ Token= token};
                    
                    response.Data = TokenDto; 
                    response.IsSuccess = true;
                    response.Message = "Autenticacion Exitosa";
                }

            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Usuario no existe";
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();

            }
            return response;
        }

        public Task<Response<bool>> DeleteUsuario(string UsuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> GuardarUsuario(UsuarioDto UsuarioDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponsePagination<IEnumerable<UsuarioDto>>> ObtenerTodoConPaginación(int NumeroDePagina, int TamañoDePagina)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<UsuarioDto>>> ObtenerTodosLosUsuarios()
        {
            throw new NotImplementedException();
        }

        public Task<Response<UsuarioDto>> ObtenerUsuario(string UsuarioId)
        {
            throw new NotImplementedException();
        }

        private string GenerateJwtToken(long IdUsuario, string Correo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Clave para firmar el token
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            // Clave para cifrar la carga útil
            var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:EncryptionKey"]));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, IdUsuario.ToString()),
                new Claim("Correo", Correo)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtExpirationMinutes), //con valor de 15 dura un minuto
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature),
                EncryptingCredentials = new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512)
            };

            try
            {
                var token = tokenHandler.CreateToken(tokenDescriptor);

                // Escribir el token
                var encryptedToken = tokenHandler.WriteToken(token);

                return encryptedToken;
            }
            catch (Exception ex)
            {
                // Maneja la excepción según tus necesidades
                Console.WriteLine($"Error al generar el token: {ex.Message}");
                return null;
            }
        }
    }
}
