using System;
using System.Collections.Generic;
using Autenticacion.Api.Dominio.Modelos.Dominio.Autenticacion.Api.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Api.Dominio.Modelos.Dominio.Autenticacion.Api.Dominio.Interfaces;

public partial class AutenticacionDbContext : DbContext
{
    public AutenticacionDbContext(DbContextOptions<AutenticacionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
