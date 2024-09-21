using Autenticacion.Api.Dominio.Persistencia.Interfaces;
using Autenticacion.Api.Dominio.Persistencia.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Api.Dominio.Persistencia.DbContextMigraciones;

public partial class AutenticacionDbContext : DbContext, IAutenticacionDbContext
{
    public AutenticacionDbContext(DbContextOptions<AutenticacionDbContext> options)
        : base(options)
    {
    }

    public DbSet<Persona> Personas { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Usuario> Usuarios { get; set; } 

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await base.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            var mensaje = $"existe un campo que infringe las restriciones de la base de datos: {ex.Message}";
            throw new DbUpdateException(mensaje, ex);
        }
        catch (Exception ex)
        {
            var message = $"Ocurrió un error al guardar los cambios: {ex.Message}";
            throw new Exception(message, ex);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Personas__A47881416D754B94");

            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.Foto).HasColumnName("foto");
            entity.Property(e => e.NombreFoto)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombreFoto");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("primerApellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("primerNombre");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("segundoApellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("segundoNombre");
            entity.Property(e => e.Telefono)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__3C872F7605F94F84");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.EsSuperUsuario)
                .HasDefaultValue(true)
                .HasColumnName("esSuperUsuario");
            entity.Property(e => e.EstadoEliminado)
                .HasDefaultValue(false)
                .HasColumnName("estadoEliminado");
            entity.Property(e => e.FechaDeActualizado).HasColumnName("fechaDeActualizado");
            entity.Property(e => e.FechaDeRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fechaDeRegistro");
            entity.Property(e => e.HoraDeActualizado).HasColumnName("horaDeActualizado");
            entity.Property(e => e.HoraDeRegistro)
                .HasDefaultValueSql("(CONVERT([time],getdate()))")
                .HasColumnName("horaDeRegistro");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ipDeActualizado");
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ipDeRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("usuarioQueActualiza");
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("usuarioQueRegistra");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuariosPkey");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__2A586E0BD9E12F9C").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Contraseña)
                .HasColumnType("text")
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.EstadoEliminado)
                .HasDefaultValue(true)
                .HasColumnName("estadoEliminado");
            entity.Property(e => e.FechaDeActualizado).HasColumnName("fechaDeActualizado");
            entity.Property(e => e.FechaDeRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fechaDeRegistro");
            entity.Property(e => e.HoraDeActualizado).HasColumnName("horaDeActualizado");
            entity.Property(e => e.HoraDeRegistro)
                .HasDefaultValueSql("(CONVERT([time],getdate()))")
                .HasColumnName("horaDeRegistro");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ipDeActualizado");
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ipDeRegistro");
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("usuarioQueActualiza");
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("usuarioQueRegistra");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPersonas");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkRoles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
