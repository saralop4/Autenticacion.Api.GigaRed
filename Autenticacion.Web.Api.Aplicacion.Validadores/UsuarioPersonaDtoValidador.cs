using Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Autenticacion.Web.Api.Aplicacion.Validadores
{
    public class UsuarioPersonaDtoValidador : AbstractValidator<UsuarioPersonaDto>
    {
        public UsuarioPersonaDtoValidador()
        {  

            RuleFor(u => u.IdIndicativo)
                 .NotEmpty().WithMessage("Debe seleccionar el indicativo.")
                 .NotNull().WithMessage("El indicativo no puede ser nulo.");

            RuleFor(u => u.IdCiudad)
                .NotEmpty().WithMessage("Debe seleccionar la ciudad.")
                .NotNull().WithMessage("El indicativo no puede ser nulo.");

            RuleFor(u => u.PrimerNombre)
                .NotEmpty().WithMessage("El primer nombre es obligatorio.")
                .NotNull().WithMessage("El primer nombre no puede ser nulo.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El primer nombre solo puede contener letras.");

            RuleFor(u => u.SegundoNombre)
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El segundo nombre solo puede contener letras.");

            RuleFor(u => u.PrimerApellido)
                .NotEmpty().WithMessage("El primer apellido es obligatorio.")
                .NotNull().WithMessage("El primer apellido no puede ser nulo.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El primer apellido solo puede contener letras.");

            RuleFor(u => u.SegundoApellido)
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El segundo apellido solo puede contener letras.");

            RuleFor(u => u.Telefono)
                .NotEmpty().WithMessage("El telefono es obligatorio.")
                .NotNull().WithMessage("El telefono no puede ser nulo.")
                .Must(SoloNumeros).WithMessage("El telefono solo puede contener números.");

            RuleFor(u => u.UsuarioQueRegistraPersona)
                .NotEmpty().WithMessage("El usuario que registra persona es obligatorio.")
                .NotNull().WithMessage("El usuario que registra persona no puede ser nulo.")
                .MaximumLength(80).WithMessage("El usuario que registra no puede tener más de 80 caracteres.")
                .Must(CorreoValido).WithMessage("El usuario que registra debe tener un formato válido. (ejemplo@dominio.com)");

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

            RuleFor(u => u.UsuarioQueRegistraUsuario)
                .NotEmpty().WithMessage("El usuario que registra usuario es obligatorio.")
                .NotNull().WithMessage("El usuario que registra usuario no puede ser nulo.")
                .MaximumLength(80).WithMessage("El usuario que usuario no puede tener más de 80 caracteres.")
                .Must(CorreoValido).WithMessage("El usuario que usuario debe tener un formato válido. (ejemplo@dominio.com)");

        }

        private bool CorreoValido(string correo)
        {
            var correoValido = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, correoValido);
        }
        private bool ContieneNumeroYCaracteresEspeciales(string contraseña)
        {
            var regex = new Regex(@"^(?=.*[0-9])(?=.*[!@#$%^&*(),.?""\\:{}|<>]).+$");
            return regex.IsMatch(contraseña);
        }
        private bool SoloNumeros(string telefono)
        {
            return telefono.All(char.IsDigit);
        }

        //public bool SoloNumerosLong(long idPersona)
        //{
        //    return idPersona.ToString().All(char.IsDigit);
        //}
    }
}