using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionCentroMedico.Models;

public partial class GestionTurnosContext : DbContext
{
    public GestionTurnosContext()
    {
    }

    public GestionTurnosContext(DbContextOptions<GestionTurnosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Mutual> Mutuales { get; set; }

    public virtual DbSet<RolesSistema> RolesSistemas { get; set; }

    public virtual DbSet<SistemaUsuario> SistemaUsuarios { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CliId);

            entity.Property(e => e.CliId).HasColumnName("cliId");
            entity.Property(e => e.CliActivo).HasColumnName("cliActivo");
            entity.Property(e => e.CliApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cliApellido");
            entity.Property(e => e.CliEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cliEmail");
            entity.Property(e => e.CliNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cliNombre");
            entity.Property(e => e.MedId).HasColumnName("medId");
            entity.Property(e => e.MutId).HasColumnName("mutId");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.MedId);

            entity.Property(e => e.MedId).HasColumnName("medId");
            entity.Property(e => e.MedActivo).HasColumnName("medActivo");
            entity.Property(e => e.MedApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("medApellido");
            entity.Property(e => e.MedEspecialidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("medEspecialidad");
            entity.Property(e => e.MedMatricula)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("medMatricula");
            entity.Property(e => e.MedNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("medNombre");
        });

        modelBuilder.Entity<Mutual>(entity =>
        {
            entity.HasKey(e => e.MutId);

            entity.Property(e => e.MutId).HasColumnName("mutId");
            entity.Property(e => e.MutActivo)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.MutDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mutDescripcion");
            entity.Property(e => e.MutNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mutNombre");
            entity.Property(e => e.MutValor).HasColumnName("mutValor");
        });

        modelBuilder.Entity<RolesSistema>(entity =>
        {
            entity.HasKey(e => e.RolId);

            entity.ToTable("RolesSistema");

            entity.Property(e => e.RolId).HasColumnName("rolId");
            entity.Property(e => e.RolDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rolDescripcion");
        });

        modelBuilder.Entity<SistemaUsuario>(entity =>
        {
            entity.HasKey(e => e.SisId).HasName("PK_Sistema");

            entity.Property(e => e.SisId).HasColumnName("sisId");
            entity.Property(e => e.RolId).HasColumnName("rolId");
            entity.Property(e => e.SisNombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sisNombreUsuario");
            entity.Property(e => e.SisPassword).HasColumnName("sisPassword");

            entity.HasOne(d => d.Rol).WithMany(p => p.SistemaUsuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sistema_RolesSistema");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.TurId);

            entity.Property(e => e.TurId).HasColumnName("turId");
            entity.Property(e => e.CliId).HasColumnName("cliId");
            entity.Property(e => e.MedId).HasColumnName("medId");
            entity.Property(e => e.MutId).HasColumnName("mutId");
            entity.Property(e => e.TurDescuentaPrepaga).HasColumnName("turDescuentaPrepaga");
            entity.Property(e => e.TurFecha)
                .HasColumnType("date")
                .HasColumnName("turFecha");
            entity.Property(e => e.TurPagoEfectivo).HasColumnName("turPagoEfectivo");
            entity.Property(e => e.TurValor).HasColumnName("turValor");

            entity.HasOne(d => d.Cli).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.CliId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turnos_Clientes");

            entity.HasOne(d => d.Med).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.MedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turnos_Medicos");

            entity.HasOne(d => d.Mut).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.MutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turnos_Mutuales");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
