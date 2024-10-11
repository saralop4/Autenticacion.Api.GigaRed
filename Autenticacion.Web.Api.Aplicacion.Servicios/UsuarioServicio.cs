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
using Autenticacion.Web.Api.Transversal.Interfaces;

namespace Autenticacion.Web.Api.Aplicacion.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _UsuarioRepositorio;
        private readonly IniciarSesionDtoValidador _IniciarSesionDtoValidador;
        private readonly UsuarioPersonaDtoValidador _UsuarioPersonaDtoValidador;
        private readonly IMenuRepositorio _MenuRepositorio;
        private readonly AppSettings _appSettings;
        private readonly IAppLogger<UsuarioServicio> _logger;


        public UsuarioServicio(IAppLogger<UsuarioServicio> logger,IMenuRepositorio MenuRepositorio, IOptions<AppSettings> appSettings, IUsuarioRepositorio UsuarioRepositorio,
            IniciarSesionDtoValidador IniciarSesionDtoValidador, UsuarioPersonaDtoValidador UsuarioPersonaDtoValidador)
        {
            _logger = logger;
            _MenuRepositorio = MenuRepositorio;
            _appSettings = appSettings.Value;
            _UsuarioRepositorio = UsuarioRepositorio;
            _IniciarSesionDtoValidador = IniciarSesionDtoValidador;
            _UsuarioPersonaDtoValidador = UsuarioPersonaDtoValidador;
        }

        public Task<Response<bool>> ActualizarUsuario(UsuarioPersonaDto UsuarioDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<TokenDto>> AutenticarUsuario(IniciarSesionDto iniciarSesionDto)
        {
            var response = new Response<TokenDto>();
            var validation = _IniciarSesionDtoValidador.Validate(new IniciarSesionDto() { Correo = iniciarSesionDto.Correo, Contraseña = iniciarSesionDto.Contraseña }); //esta variable almacena la respuesta del validador fluent

            if (!validation.IsValid)
            {
                response.Message = "Errores de validación encontrados";
                response.Errors = validation.Errors;
                return response;
            }

            try
            {
                var usuarioValidado = await _UsuarioRepositorio.ValidarUsuario(iniciarSesionDto);

                if (usuarioValidado is not null) 
                {
                    var menus = await _MenuRepositorio.ObtenerMenusPorRol(usuarioValidado.IdRol);

                  //  Console.WriteLine(JsonConvert.SerializeObject(menus));

                    string token = GenerateJwtToken(usuarioValidado.IdUsuario, usuarioValidado.Rol, usuarioValidado.Correo, menus);
                    TokenDto TokenDto = new TokenDto { Token = token };

                    response.Data = TokenDto;
                    response.IsSuccess = true;
                    response.Message = "Autenticacion exitosa";
                    _logger.LogInformation("Autenticacion exitosa!!");
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

        public async Task<Response<bool>> RegistrarUsuario(UsuarioPersonaDto UsuarioDto)
        {
            var response = new Response<bool>();

            try
            {
                var validation = _UsuarioPersonaDtoValidador.Validate(new UsuarioPersonaDto()
                {

                    IdIndicativo = UsuarioDto.IdIndicativo,
                    IdCiudad = UsuarioDto.IdCiudad,
                    PrimerNombre = UsuarioDto.PrimerNombre,
                    SegundoNombre = UsuarioDto.SegundoNombre,
                    PrimerApellido = UsuarioDto.PrimerApellido,
                    SegundoApellido = UsuarioDto.SegundoApellido,
                    Telefono = UsuarioDto.Telefono,
                    UsuarioQueRegistraPersona = UsuarioDto.UsuarioQueRegistraPersona,
                    Correo = UsuarioDto.Correo,
                    Contraseña = UsuarioDto.Contraseña,
                    UsuarioQueRegistraUsuario = UsuarioDto.UsuarioQueRegistraUsuario
                });

                if (!validation.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = "Errores de validación";
                    response.Errors = validation.Errors;
                    return response;
                }

                var usuarioExistente = await _UsuarioRepositorio.Obtener(UsuarioDto.Correo);

                if (usuarioExistente is not null)
                {
                    response.IsSuccess = false;
                    response.Message = "El usuario ya existe";
                    return response;
                }

                var Usuario = await _UsuarioRepositorio.Guardar(UsuarioDto);

                if (Usuario is {})
                {
                    response.IsSuccess = true;
                    response.Message = "Registro exitoso!";
                    _logger.LogInformation("Registro exitosa!!");
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
                _logger.LogError(ex.Message);
            }

            return response;
        }

      
        private string GenerateJwtToken(long idUsuario, string rol, string correo, List<MenuDto> menus)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Clave para firmar el token
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, idUsuario.ToString()),
                new Claim("Correo", correo),
                new Claim("Rol", rol)
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
