using Autenticacion.Web.Api.Dominio.DTOs;
using Autenticacion.Web.Api.Dominio.Interfaces;
using Autenticacion.Web.Api.Dominio.Persistencia;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Autenticacion.Web.Api.Infraestructura.Repositorios;
public class IndicativoRepositorio : IIndicativoRepositorio
{
    private readonly DapperContext _context;
    public IndicativoRepositorio(IConfiguration configuration)
    {
        _context = new DapperContext(configuration);
    }

    public async Task<IEnumerable<IndicativoDto>> ObtenerTodo()
    {
        using (var conexion = _context.CreateConnection())
        {
            var query = "SELECT * FROM Indicativos";
            var resultado = await conexion.QueryAsync<IndicativoDto>(query);

            return resultado;
        }
    }

}
