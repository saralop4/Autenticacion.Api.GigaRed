using Autenticacion.Api.Aplicacion.Servicios;
using Autenticacion.Api.Dominio.DTOs;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Autenticacion.Api.Controllers.V1
{
    //[Authorize]
    [Route("Api/V{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")] 
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioServicio _UsuarioServicio;  
        public UsuarioController(UsuarioServicio UsuarioServicio) 
        {
            _UsuarioServicio = UsuarioServicio;
        }


        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] IniciarSesionDto dto)
        {
            if (dto is not { })
            {
                return BadRequest();
            }
                var response = await _UsuarioServicio.AutenticarUsuario(dto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
        }

        [HttpPost("RegistrarUsuario")]
        public async Task<IActionResult> InsertAsync([FromBody] UsuarioDto UsuarioDto)
        {
            if (UsuarioDto == null)
            {
                return BadRequest();
            }
            var response = await _UsuarioServicio.GuardarUsuario(UsuarioDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }
    }

}
