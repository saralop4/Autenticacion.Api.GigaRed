using Autenticacion.Api.Dominio.Persistencia.Modelos;
using Microsoft.EntityFrameworkCore;


namespace Autenticacion.Api.Dominio.Persistencia.Interfaces
{
    public interface IAutenticacionDbContext
    {

        public  DbSet<Persona> Personas { get; set; }

        public  DbSet<Role> Roles { get; set; }

        public  DbSet<Usuario> Usuarios { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Dispose();
    }
}
