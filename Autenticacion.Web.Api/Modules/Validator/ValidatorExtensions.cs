using Autenticacion.Web.Api.Aplicacion.Validadores;

namespace Autenticacion.Web.Api.Modules.Validator;
public static class ValidatorExtensions
{
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddTransient<IniciarSesionDtoValidador>(); //crea una instancia por cada peticion
        services.AddTransient<UsuarioDtoValidador>();

        return services;
    }
}
