using Autenticacion.Api.Aplicacion.Validadores;

namespace Autenticacion.Api.Modules.Validator

{
    public static class ValidatorExtensions 
    {

        public static IServiceCollection AddValidator(this IServiceCollection services) 
        { 
            services.AddTransient<IniciarSesionDtoValidador>(); //crea una instancia por cada peticion
            services.AddTransient<UsuarioDtoValidador>();
            services.AddTransient<PersonaDtoValidador>();

            return services;    
        }
    }
}
