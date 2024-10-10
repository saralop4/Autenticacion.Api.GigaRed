using Autenticacion.Web.Api.Modules.Authentication;
using Autenticacion.Web.Api.Modules.Injection;
using Autenticacion.Web.Api.Modules.Swagger;
using Autenticacion.Web.Api.Modules.Validator;
using Autenticacion.Web.Api.Modules.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Autenticacion.Web.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddVersioning();
        builder.Services.AddAuthentication(builder.Configuration);
        builder.Services.AddValidator();
        builder.Services.AddInjection(builder.Configuration);
        builder.Services.AddSwaggerDocumentation();

        builder.Services.AddCors(option =>
        {
            option.AddPolicy("policyApi", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });


        var app = builder.Build();

        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {

            app.UseDeveloperExceptionPage();
            app.UseSwagger(); //habilitamos el middleware para servir al swagger generated como un endpoint json
            app.UseSwaggerUI( // habilitamos el dashboard de swagger 
                c =>
                {
                    //SwaggerEndpoint ese metodo recibe dos parametros, el primero es la url, el segundo es el nombre del endpoint
                    //  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi Api Empresarial v1");

                    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }

        app.UseHttpsRedirection();
       // app.UseAuthentication();
        app.UseCors("policyApi");
       // app.UseAuthorization();
        app.MapControllers();
        app.Run();

    }
}
