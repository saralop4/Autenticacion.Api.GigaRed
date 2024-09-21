
namespace Autenticacion.Api.Modules.Injection
{
    public static class InjectionExtensions
    {

        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddSingleton<DapperContext>(); //se necesita que una sola vez se conecte a la baase de datos y
            //y esa misma instancia de conexion se reutilice
            services.AddScoped<ICustomerAplicacion, CustomersAplicacion>();
            services.AddScoped<ICustomersDomain, CustomersDomain>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IUsersAplicacion, UsersAplicacion>();
            services.AddScoped<IUsersDomain, UsersDomain>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<UsersDtoValidator>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();   

            return services;    
        }


    }
}
