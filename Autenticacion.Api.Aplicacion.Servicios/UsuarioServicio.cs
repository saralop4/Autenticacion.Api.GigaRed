using Autenticacion.Api.Aplicacion.Interfaces;
using Autenticacion.Api.Dominio.Validador;
using Autenticacion.Api.Dominio.DTOs;
using Autenticacion.Api.Infraestructura.Interfaces;
using Autenticacion.Api.Transversal.Modelos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;

namespace Autenticacion.Api.Aplicacion.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _UsuarioRepositorio;
        private readonly IniciarSesionDtoValidador _IniciarSesionDtoValidador;
        private readonly AppSettings _appSettings;

        public UsuarioServicio(IOptions<AppSettings> appSettings, IUsuarioRepositorio UsuarioRepositorio, IniciarSesionDtoValidador IniciarSesionDtoValidador)
        {
            _appSettings = appSettings.Value;
            _UsuarioRepositorio = UsuarioRepositorio;
            _IniciarSesionDtoValidador = IniciarSesionDtoValidador;
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

        public async Task<Response<bool>> GuardarUsuario(UsuarioDto UsuarioDto)
        {
            var response = new Response<bool>();

            try
            {
                //var Usuario = _mapper.Map<Usuario>(UsuarioDto);

                response.Data = await _UsuarioRepositorio.Guardar(UsuarioDto);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }
            return response;
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
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));

            // Clave para cifrar la carga útil
         //   var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:EncryptionKey"]));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, IdUsuario.ToString()),
                new Claim("Correo", Correo)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15), 
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
                // EncryptingCredentials = new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512)
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
