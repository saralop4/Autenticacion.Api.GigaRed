using Autenticacion.Api.Dominio.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Autenticacion.Api.Dominio.Validadores
{
    public class UsuarioDtoValidador : AbstractValidator<UsuarioDto>
    {
        public UsuarioDtoValidador()
        {
            RuleFor(u => u.IdPersona)
               .NotEmpty().WithMessage("Debe proporcionar el id de la persona")
               .NotNull().WithMessage("El id de la persona no puede ser nula.");

            RuleFor(u => u.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .NotNull().WithMessage("El correo no puede ser nulo.")
                .Must(BeAValidEmail).WithMessage("El correo debe tener un formato. (ejemplo@dominio.com)");

            RuleFor(u => u.Contraseña)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .NotNull().WithMessage("La contraseña no puede ser nula.");

            RuleFor(u => u.UsuarioQueRegistra)
               .NotEmpty().WithMessage("El usuario que registra es obligatorio.")
               .NotNull().WithMessage("El usuario que registra  no puede ser nulo.")
               .Must(BeAValidEmail).WithMessage("El usuario que registra debe tener un formato. (ejemplo@dominio.com)");

        }

        private bool BeAValidEmail(string Correo)
        {
            // Expresión regular para validar el formato del correo
            var CorreoValido = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(Correo, CorreoValido);
        }

    }
}
