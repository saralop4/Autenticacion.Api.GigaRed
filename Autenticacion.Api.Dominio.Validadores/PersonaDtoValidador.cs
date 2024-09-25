using Autenticacion.Api.Dominio.DTOs.PersonaDTOS;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Autenticacion.Api.Dominio.Validadores
{
    public class PersonaDtoValidador : AbstractValidator<PersonaDto>
    {
        public PersonaDtoValidador()
        {
            RuleFor(u => u.PrimerNombre)
                .NotEmpty().WithMessage("El primer nombre es obligatorio.")
                .NotNull().WithMessage("El primer nombre no puede ser nulo.")
                .Matches("^[a-zA-Z]+$").WithMessage("El primer nombre solo puede contener letras.")
                .MaximumLength(30).WithMessage("El primer nombre no puede tener más de 30 caracteres.");


            RuleFor(u => u.PrimerApellido)
                .NotEmpty().WithMessage("El primer apellido es obligatorio.")
                .NotNull().WithMessage("El primer apellido no puede ser nulo.")
                .Matches("^[a-zA-Z]+$").WithMessage("El primer apellido solo puede contener letras.")
                .MaximumLength(30).WithMessage("El primer apellido no puede tener más de 30 caracteres.");
               

            RuleFor(u => u.Telefono)
                .NotEmpty().WithMessage("Debe proporcionar un IdPersona.")
                .NotNull().WithMessage("El telefono que registra no puede ser nulo.")
                .Must(SoloNumeros).WithMessage("El telefono solo puede contener 10 números.")
                .GreaterThan(0).WithMessage("Debe proporcionar un numero válido y mayor que 0.");

            RuleFor(u => u.UsuarioQueRegistra)
              .NotEmpty().WithMessage("El usuario que registra es obligatorio.")
              .NotNull().WithMessage("El usuario que registra no puede ser nulo.")
              .MaximumLength(80).WithMessage("El usuario que registra no puede tener más de 80 caracteres.")
              .Must(CorreoValido).WithMessage("El usuario que registra debe tener un formato válido. (ejemplo@dominio.com)");

        }
        public bool SoloNumeros(decimal telefono)
        {
            return telefono.ToString().All(char.IsDigit);
        }
        private bool CorreoValido(string correo)
        {
            // Expresión regular para validar el formato del correo
            var correoValido = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, correoValido);
        }
    }
}
