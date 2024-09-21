using Autenticacion.Api.Dominio.DTOs;
using FluentValidation;

namespace Autenticacion.Api.Aplicacion.Validador
{
    public class IniciarSesionDtoValidador : AbstractValidator<IniciarSesionDto>
    {
        public IniciarSesionDtoValidador() 
        {
            RuleFor(u => u.Correo).NotEmpty().NotNull();
            RuleFor(u => u.Contraseña).NotEmpty().NotNull();
        }

    }
}
