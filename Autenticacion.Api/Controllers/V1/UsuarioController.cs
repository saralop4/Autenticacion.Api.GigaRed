using Microsoft.AspNetCore.Mvc;

namespace Autenticacion.Api.Controllers.V1
{
    //[Authorize]
    [Route("Api/V{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")] 
    public class UsuarioController : ControllerBase
    {

    }
}
