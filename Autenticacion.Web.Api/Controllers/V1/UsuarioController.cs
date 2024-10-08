using Autenticacion.Web.Api.Aplicacion.Interfaces;
using Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacion.Web.Api.Controllers.V1
{
    [Route("Api/V{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServicio _IUsuarioServicio;
        public UsuarioController(IUsuarioServicio UsuarioServicio)
        {
            _IUsuarioServicio = UsuarioServicio;
        }


        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] IniciarSesionDto dto)
        {
            var response = await _IUsuarioServicio.AutenticarUsuario(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }

        [HttpPost("RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioDto usuarioDto)
        {
            // Obtener la IP
            var ipDeRegistro = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (ipDeRegistro != null)
            {
                usuarioDto.IpDeRegistro = ipDeRegistro;
            }

            // Console.WriteLine(JsonConvert.SerializeObject(usuarioDto));

            var response = await _IUsuarioServicio.RegistrarUsuario(usuarioDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

    }

}
