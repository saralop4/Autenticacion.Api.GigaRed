using Autenticacion.Api.Dominio.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Autenticacion.Api.Dominio.Validadores
{
    public class UsuarioDtoValidador : AbstractValidator<UsuarioDto>
    {
        public UsuarioDtoValidador()
        {
            // Validación de IdPersona (debe ser mayor que 0)
            RuleFor(u => u.IdPersona)
               .GreaterThan(0).WithMessage("Debe proporcionar un IdPersona válido y mayor que 0.");


            RuleFor(u => u.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .NotNull().WithMessage("El correo no puede ser nulo.")
                .MaximumLength(80).WithMessage("El correo no puede tener más de 80 caracteres.")
                .Must(BeAValidEmail).WithMessage("El correo debe tener un formato válido. (ejemplo@dominio.com)");


            RuleFor(u => u.Contraseña)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .NotNull().WithMessage("La contraseña no puede ser nula.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.");


            RuleFor(u => u.UsuarioQueRegistra)
               .NotEmpty().WithMessage("El usuario que registra es obligatorio.")
               .NotNull().WithMessage("El usuario que registra no puede ser nulo.")
               .MaximumLength(100).WithMessage("El usuario que registra no puede tener más de 100 caracteres.")
               .Must(BeAValidEmail).WithMessage("El usuario que registra debe tener un formato válido. (ejemplo@dominio.com)");

        }

        private bool BeAValidEmail(string correo)
        {
            // Expresión regular para validar el formato del correo
            var correoValido = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, correoValido);
        }
    }
}