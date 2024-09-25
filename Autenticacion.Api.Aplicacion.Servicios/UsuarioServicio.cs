using Autenticacion.Api.Aplicacion.Interfaces;
using Autenticacion.Api.Dominio.DTOs.UsuarioDTOS;
using Autenticacion.Api.Dominio.Validadores;
using Autenticacion.Api.Infraestructura.Interfaces;
using Autenticacion.Api.Transversal.Modelos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Autenticacion.Api.Aplicacion.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _UsuarioRepositorio;
        private readonly IniciarSesionDtoValidador _IniciarSesionDtoValidador;
        private readonly UsuarioDtoValidador _UsuarioDtoValidador;
        private readonly AppSettings _appSettings;

        public UsuarioServicio(IOptions<AppSettings> appSettings, IUsuarioRepositorio UsuarioRepositorio,
            IniciarSesionDtoValidador IniciarSesionDtoValidador, UsuarioDtoValidador UsuarioDtoValidador)
        {
            _appSettings = appSettings.Value;
            _UsuarioRepositorio = UsuarioRepositorio;
            _IniciarSesionDtoValidador = IniciarSesionDtoValidador;
            _UsuarioDtoValidador = UsuarioDtoValidador;
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
                response.Message = "Errores de validacion";
                response.Errors = validation.Errors;
                return response;
            }

            try
            {
                var usuarioValidado = await _UsuarioRepositorio.UsuarioAutenticado(IniciarSesionDto);

                if (usuarioValidado is {}) //si usuarioValidado no es nulo
                {
                    string token =  GenerateJwtToken(usuarioValidado.IdUsuario, usuarioValidado.Correo);
                    TokenDto TokenDto = new TokenDto{ Token= token};
                    
                    response.Data = TokenDto; 
                    response.IsSuccess = true;
                    response.Message = "Autenticacion exitosa";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Usuario no existe";
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

        public Task<Response<bool>> DeleteUsuario(long IdUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> RegistrarUsuario(UsuarioDto UsuarioDto)
        {
            var response = new Response<bool>();

                var validation = _UsuarioDtoValidador.Validate(new UsuarioDto()
                {
                    IdPersona = UsuarioDto.IdPersona,
                    Correo = UsuarioDto.Correo,
                    Contraseña = UsuarioDto.Contraseña,
                    UsuarioQueRegistra = UsuarioDto.UsuarioQueRegistra
                });

                if (!validation.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = "Errores de validacion";
                    response.Errors = validation.Errors;
                    return response;

                }

                var usuarioExistente = await ObtenerUsuario(UsuarioDto.Correo);

                if (usuarioExistente.IsSuccess)
                 {
                    response.IsSuccess = false;
                    response.Message = "El usuario ya existe";
                 }

                var idPersonaExistente = await _UsuarioRepositorio.ExisteIdPersona(UsuarioDto.IdPersona);

                if (idPersonaExistente)
                {
                    response.IsSuccess = false;
                    response.Message = "El id persona ya existe";
                }

                    var Usuario = await _UsuarioRepositorio.Guardar(UsuarioDto);

                    if (Usuario is {})
                    {
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
        

        public Task<ResponsePagination<IEnumerable<UsuarioDto>>> ObtenerTodoConPaginación(int NumeroDePagina, int TamañoDePagina)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<UsuarioDto>>> ObtenerTodosLosUsuarios()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<UsuarioDto>> ObtenerUsuario(string Correo)
        {
            var response = new Response<UsuarioDto>();

            try
            {
                var usuarioValidado = await _UsuarioRepositorio.Obtener(Correo);

                if (usuarioValidado is {})
                {
                    response.Data = usuarioValidado;
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Usuario no encontrado";
                }
             }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;

        }
        private string GenerateJwtToken(long IdUsuario, string Correo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Clave para firmar el token
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));

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
