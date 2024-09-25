using Autenticacion.Api.Dominio.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Autenticacion.Api.Dominio.Validadores
{
    public class IniciarSesionDtoValidador : AbstractValidator<IniciarSesionDto>
    {
        public IniciarSesionDtoValidador()
        {
            RuleFor(u => u.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .NotNull().WithMessage("El correo no puede ser nulo.")
                .MaximumLength(80).WithMessage("El correo no puede tener más de 80 caracteres.")
                .Must(BeAValidEmail).WithMessage("El correo debe tener un formato válido. (ejemplo@dominio.com)");


            RuleFor(u => u.Contraseña)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .NotNull().WithMessage("La contraseña no puede ser nula.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.");

        }

        private bool BeAValidEmail(string Correo)
        {
            // Expresión regular para validar el formato del correo
            var CorreoValido = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(Correo, CorreoValido);
        }
    }
}
