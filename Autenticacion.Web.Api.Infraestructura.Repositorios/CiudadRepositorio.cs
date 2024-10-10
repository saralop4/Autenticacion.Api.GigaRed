using Autenticacion.Web.Api.Dominio.DTOs;
using Autenticacion.Web.Api.Dominio.Interfaces;
using Autenticacion.Web.Api.Dominio.Persistencia;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Autenticacion.Web.Api.Infraestructura.Repositorios;

public class CiudadRepositorio : ICiudadRepositorio
{
    private readonly DapperContext _context;
    public CiudadRepositorio(IConfiguration configuration)
    {
        _context = new DapperContext(configuration);
    }
    public async Task<IEnumerable<CiudadDto>> ObtenerTodo()
    {
        using (var conexion = _context.CreateConnection())
        {
            var query = "ObtenerCiudadesYPaises";
            var parameters = new DynamicParameters();
            var ciudades = await conexion.QueryAsync<CiudadDto>(query, commandType: CommandType.StoredProcedure);
            return ciudades.ToList();
        }
    }
}
