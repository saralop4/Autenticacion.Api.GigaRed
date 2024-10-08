using Autenticacion.Web.Api.Aplicacion.Interfaces;
using Autenticacion.Web.Api.Dominio.DTOs.PersonaDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacion.Web.Api.Controllers.V1
{
    [Route("Api/V{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaServicio _IPersonaServicio;
        public PersonaController(IPersonaServicio IPersonaServicio)
        {
            _IPersonaServicio = IPersonaServicio;
        }

        [HttpPost("RegistrarPersona")]
        public async Task<IActionResult> RegistrarPersona([FromBody] PersonaDto PersonaDto)
        {
            if (string.IsNullOrEmpty(PersonaDto.IpDeRegistro))
            {
                var ipDeRegistro = HttpContext.Connection.RemoteIpAddress?.ToString();

                if (ipDeRegistro != null)
                {
                    PersonaDto.IpDeRegistro = ipDeRegistro;
                }
            }


            var response = await _IPersonaServicio.RegistrarPersona(PersonaDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


    }
}
