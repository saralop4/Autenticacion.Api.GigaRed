
namespace Autenticacion.Api.Modules.Validator

{
    public static class ValidatorExtensions 
    {

        public static IServiceCollection AddValidator(this IServiceCollection services) 
        { 
            services.AddTransient<UsersDtoValidator>(); //crea una instancia por cada peticion
            return services;    
        }
    }
}
