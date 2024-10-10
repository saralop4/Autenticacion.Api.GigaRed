using Autenticacion.Web.Api.Transversal.Interfaces;
using Autenticacion.Web.Api.Aplicacion.Interfaces;
using Autenticacion.Web.Api.Aplicacion.Servicios;
using Autenticacion.Web.Api.Dominio.Interfaces;
using Autenticacion.Web.Api.Dominio.Persistencia;
using Autenticacion.Web.Api.Infraestructura.Repositorios;
using Autenticacion.Web.Api.Transversal.Modelos;


namespace Autenticacion.Web.Api.Modules.Injection;

public static class InjectionExtensions
{

    public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddSingleton<DapperContext>();
        services.AddScoped<IUsuarioServicio, UsuarioServicio>();
        services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        services.AddScoped<IMenuRepositorio, MenuRepositorio>();
        services.AddScoped<ICiudadRepositorio, CiudadRepositorio>();
        services.AddScoped<ICiudadServicio, CiudadServicio>();
        services.AddScoped<IIndicativoServicio, IndicativoServicio>();
        services.AddScoped<IIndicativoRepositorio, IndicativoRepositorio>();

        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }


}
