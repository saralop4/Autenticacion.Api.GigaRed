using System;
using System.Collections.Generic;
using Autenticacion.Api.Dominio.Persistencia.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Api.Dominio.Persistencia.DbContextMigraciones;

public partial class AutenticacionDbContext : DbContext
{
    public AutenticacionDbContext(DbContextOptions<AutenticacionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Personas__2EC8D2ACE67AD0C2");

            entity.Property(e => e.EstadoEliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaDeRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HoraDeRegistro).HasDefaultValueSql("(CONVERT([time],getdate()))");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.NombreFoto)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Telefono).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584C9542CEF7");

            entity.Property(e => e.EsSuperUsuario).HasDefaultValue(true);
            entity.Property(e => e.EstadoEliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaDeRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HoraDeRegistro).HasDefaultValueSql("(CONVERT([time],getdate()))");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("UsuariosPkey");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A196EF41F2C").IsUnique();

            entity.Property(e => e.Contraseña).IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.EstadoEliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaDeRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HoraDeRegistro).HasDefaultValueSql("(CONVERT([time],getdate()))");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkPersonas");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkRoles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
