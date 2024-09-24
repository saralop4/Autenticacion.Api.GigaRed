using Autenticacion.Api.Aplicacion.Interfaces;
using Autenticacion.Api.Dominio.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacion.Api.Controllers.V1
{
    //[Authorize]
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
            if (dto is not { })
            {
                return BadRequest();
            }
                var response = await _IUsuarioServicio.AutenticarUsuario(dto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
        }

        [HttpPost("RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioDto UsuarioDto)
        {
            if (UsuarioDto == null)
            {
                return BadRequest();
            }
            var response = await _IUsuarioServicio.GuardarUsuario(UsuarioDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }
    }

}
