
using Autenticacion.Api.Aplicacion.Interfaces;
using Autenticacion.Api.Aplicacion.Servicios;
using Autenticacion.Api.Aplicacion.Validador;
using Autenticacion.Api.Dominio.Repositorios;
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
          //  services.AddSingleton<DapperContext>(); //se necesita que una sola vez se conecte a la baase de datos y
            //y esa misma instancia de conexion se reutilice
            services.AddScoped<IUsuario, UsuarioServicio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IniciarSesionDtoValidador>(); 

            return services;    
        }


    }
}
