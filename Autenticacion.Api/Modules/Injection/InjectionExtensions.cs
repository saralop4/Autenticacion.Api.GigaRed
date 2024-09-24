using Autenticacion.Api.Aplicacion.Interfaces;
using Autenticacion.Api.Aplicacion.Servicios;
using Autenticacion.Api.Dominio.Persistencia;
using Autenticacion.Api.Dominio.Repositorios;
using Autenticacion.Api.Dominio.Validador;
using Autenticacion.Api.Infraestructura.Interfaces;
using Autenticacion.Api.Transversal.Interfaces;
using Autenticacion.Api.Transversal.Modelos;


namespace Autenticacion.Api.Modules.Injection
{
    public static class InjectionExtensions
    {

        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddSingleton<DapperContext>();
            services.AddScoped<IUsuarioServicio, UsuarioServicio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IniciarSesionDtoValidador>(); 

            return services;    
        }


    }
}
