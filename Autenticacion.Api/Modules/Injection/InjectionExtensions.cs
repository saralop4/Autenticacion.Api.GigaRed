using Autenticacion.Api.Aplicacion.Interfaces;
using Autenticacion.Api.Aplicacion.Servicios;
using Autenticacion.Api.Dominio.Interfaces;
using Autenticacion.Api.Dominio.Persistencia;
using Autenticacion.Api.Infraestructura.Repositorios;
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
            services.AddScoped<IPersonaServicio, PersonaServicio>();
            services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
            services.AddScoped<IMenuRepositorio, MenuRepositorio>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            //services.AddTransient<IniciarSesionDtoValidador>();
            //services.AddTransient<UsuarioDtoValidador>();
            //services.AddTransient<PersonaDtoValidador>();

            return services;    
        }


    }
}
