using Autenticacion.Web.Api.Aplicacion.Interfaces;
using Autenticacion.Web.Api.Transversal.Modelos;
using Autenticacion.Web.Api.Aplicacion.Validadores;
using Autenticacion.Web.Api.Dominio.DTOs;
using Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;
using Autenticacion.Web.Api.Dominio.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Autenticacion.Web.Api.Aplicacion.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _UsuarioRepositorio;
        private readonly IniciarSesionDtoValidador _IniciarSesionDtoValidador;
        private readonly UsuarioDtoValidador _UsuarioDtoValidador;
        private readonly IMenuRepositorio _MenuRepositorio;
        private readonly AppSettings _appSettings;

        public UsuarioServicio(IMenuRepositorio MenuRepositorio, IOptions<AppSettings> appSettings, IUsuarioRepositorio UsuarioRepositorio,
            IniciarSesionDtoValidador IniciarSesionDtoValidador, UsuarioDtoValidador UsuarioDtoValidador)
        {
            _MenuRepositorio = MenuRepositorio;
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
                response.Message = "Errores de validación encontrados";
                response.Errors = validation.Errors;
                return response;
            }

            try
            {
                var usuarioValidado = await _UsuarioRepositorio.UsuarioAutenticado(IniciarSesionDto);

                if (usuarioValidado is {}) //si usuarioValidado no es nulo
                {
                    var menus = await _MenuRepositorio.ObtenerMenusPorRol(usuarioValidado.IdRol);

                    string token = GenerateJwtToken(usuarioValidado.IdUsuario, usuarioValidado.IdRol, usuarioValidado.Correo, menus);
                    TokenDto TokenDto = new TokenDto { Token = token };

                    response.Data = TokenDto;
                    response.IsSuccess = true;
                    response.Message = "Autenticacion exitosa";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Usuario o Contraseña Incorrectos";
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

            try
            {
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
                    response.Message = "Errores de validación";
                    response.Errors = validation.Errors;
                    return response;
                }

                var usuarioExistente = await ObtenerUsuario(UsuarioDto.Correo);
                if (usuarioExistente.IsSuccess)
                {
                    response.IsSuccess = false;
                    response.Message = "El usuario ya existe";
                    return response;
                }

                var idPersonaExistente = await _UsuarioRepositorio.ExisteIdPersona(UsuarioDto.IdPersona);
                if (idPersonaExistente)
                {
                    response.IsSuccess = false;
                    response.Message = "El ID de persona ya existe";
                    return response; // Evitamos continuar si el ID de persona ya existe
                }

                var Usuario = await _UsuarioRepositorio.Guardar(UsuarioDto);

                if (Usuario is { })
                {
                    response.IsSuccess = true;
                    response.Message = "Registro exitoso!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Hubo un error al crear el registro";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Ocurrió un error: {ex.Message}";
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

                if (usuarioValidado is { })
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

        private string GenerateJwtToken(long IdUsuario, long IdRol, string Correo, List<MenuDto> menus)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Clave para firmar el token
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, IdUsuario.ToString()),
                new Claim("Correo", Correo),
                new Claim("IdRol", IdRol.ToString())
            };

            // Convertir los menús a JSON
            var menuJson = JsonConvert.SerializeObject(menus);

            // Añadir el menú encriptado al token como un nuevo claim
            claims.Add(new Claim("Menus", menuJson));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };

            try
            {
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var encryptedToken = tokenHandler.WriteToken(token);
                return encryptedToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el token: {ex.Message}");
                return null;
            }
        }

    }
}
