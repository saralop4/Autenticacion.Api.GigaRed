using Autenticacion.Web.Api.Aplicacion.Interfaces;
using Autenticacion.Web.Api.Dominio.DTOs;
using Autenticacion.Web.Api.Dominio.Interfaces;
using Autenticacion.Web.Api.Transversal.Interfaces;
using Autenticacion.Web.Api.Transversal.Modelos;

namespace Autenticacion.Web.Api.Aplicacion.Servicios
{
    public class CiudadServicio : ICiudadServicio
    {
        private readonly ICiudadRepositorio _ciudadRepositorio;
        private readonly IAppLogger<CiudadServicio> _logger;
        public CiudadServicio(ICiudadRepositorio ciudadRepositorio, IAppLogger<CiudadServicio> logger)
        {
            _ciudadRepositorio = ciudadRepositorio;
            _logger = logger;

        }
        public async Task<Response<IEnumerable<CiudadDto>>> ObtenerTodos()
        {
            var response = new Response<IEnumerable<CiudadDto>>();

            try
            {
                var resultado = await _ciudadRepositorio.ObtenerTodo();

                if (resultado != null && resultado.Any())
                {
                    response.Data = resultado;
                    response.IsSuccess = true;
                    _logger.LogInformation("Consulta exitosa!!");
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No hay información disponible";
                    _logger.LogInformation("La consulta de obtener todo de base de datos está vacia");
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

    }
}
