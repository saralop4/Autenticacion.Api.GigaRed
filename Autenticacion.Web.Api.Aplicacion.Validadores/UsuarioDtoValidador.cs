using Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Autenticacion.Web.Api.Aplicacion.Validadores
{
    public class UsuarioDtoValidador : AbstractValidator<UsuarioDto>
    {
        public UsuarioDtoValidador()
        {
            // Validación de IdPersona (debe ser mayor que 0)
            RuleFor(u => u.IdPersona)
                .NotEmpty().WithMessage("Debe proporcionar un IdPersona.")
                .Must(SoloNumeros).WithMessage("El IdPersona solo puede contener números.")
                .GreaterThan(0).WithMessage("Debe proporcionar un IdPersona válido y mayor que 0.");


            RuleFor(u => u.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .NotNull().WithMessage("El correo no puede ser nulo.")
                .MaximumLength(80).WithMessage("El correo no puede tener más de 80 caracteres.")
                .Must(CorreoValido).WithMessage("El correo debe tener un formato válido. (ejemplo@dominio.com)");


            RuleFor(u => u.Contraseña)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .NotNull().WithMessage("La contraseña no puede ser nula.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Must(ContieneNumeroYCaracteresEspeciales).WithMessage("La contraseña debe contener al menos un número y un carácter especial.");


            RuleFor(u => u.UsuarioQueRegistra)
               .NotEmpty().WithMessage("El usuario que registra es obligatorio.")
               .NotNull().WithMessage("El usuario que registra no puede ser nulo.")
               .MaximumLength(80).WithMessage("El usuario que registra no puede tener más de 80 caracteres.")
               .Must(CorreoValido).WithMessage("El usuario que registra debe tener un formato válido. (ejemplo@dominio.com)");

        }

        private bool CorreoValido(string correo)
        {
            // Expresión regular para validar el formato del correo
            var correoValido = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, correoValido);
        }
        private bool ContieneNumeroYCaracteresEspeciales(string contraseña)
        {
            var regex = new Regex(@"^(?=.*[0-9])(?=.*[!@#$%^&*(),.?""\\:{}|<>]).+$");
            return regex.IsMatch(contraseña);
        }
        public bool SoloNumeros(long idPersona)
        {
            return idPersona.ToString().All(char.IsDigit);
        }

    }
}