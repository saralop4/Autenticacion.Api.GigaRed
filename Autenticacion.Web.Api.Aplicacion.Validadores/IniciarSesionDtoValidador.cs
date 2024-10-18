using Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Autenticacion.Web.Api.Aplicacion.Validadores
{
    public class IniciarSesionDtoValidador : AbstractValidator<IniciarSesionDto>
    {
        public IniciarSesionDtoValidador()
        {
            RuleFor(u => u.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .NotNull().WithMessage("El correo no puede ser nulo.")
                .Length(10,80).WithMessage("El correo debe ser entre 10 y  80 caracteres.")
                .EmailAddress().WithMessage("El correo debe tener un formato válido2. (ejemplo@dominio.com)");

            RuleFor(u => u.Contraseña)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .NotNull().WithMessage("La contraseña no puede ser nula.");
        }
        
    }
}
