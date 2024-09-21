﻿using Autenticacion.Api.Dominio.DTOs;

namespace Autenticacion.Api.Infraestructura.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<UsuarioDto>
    {
        public Task<UsuarioDto> UsuarioAutenticado(IniciarSesionDto IniciarSesionDto);
    }
}
