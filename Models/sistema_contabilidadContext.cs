using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ModContabilidad.Models
{
    public partial class sistema_contabilidadContext : DbContext
    {
        public sistema_contabilidadContext()
        {
        }

        public sistema_contabilidadContext(DbContextOptions<sistema_contabilidadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auxiliar> Auxiliar { get; set; }
        public virtual DbSet<CuentaContable> CuentaContable { get; set; }
        public virtual DbSet<DetalleEntradaContable> DetalleEntradaContable { get; set; }
        public virtual DbSet<EntradaContable> EntradaContable { get; set; }
        public virtual DbSet<Moneda> Moneda { get; set; }
        public virtual DbSet<TipoCuenta> TipoCuenta { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=ShespaDev07*;database=sistema_contabilidad");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auxiliar>(entity =>
            {
                entity.ToTable("auxiliar");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(130)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");
            });

            modelBuilder.Entity<CuentaContable>(entity =>
            {
                entity.ToTable("cuenta_contable");

                entity.HasIndex(e => e.TipoCuentaId)
                    .HasName("cuenta_contable_tipo_cuenta_id_fk");

                //id a mano by ignacio
                entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.CuentaMayor).HasColumnName("cuenta_mayor");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(130)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.PermiteTransaccion)
                    .HasColumnName("permite_transaccion")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.TipoCuentaId).HasColumnName("tipo_cuenta_id");

                entity.HasOne(d => d.TipoCuenta)
                    .WithMany(p => p.CuentaContable)
                    .HasForeignKey(d => d.TipoCuentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cuenta_contable_tipo_cuenta_id_fk");
            });

            modelBuilder.Entity<DetalleEntradaContable>(entity =>
            {
                entity.ToTable("detalle_entrada_contable");

                entity.HasIndex(e => e.CuentaContableId)
                    .HasName("detalle_entrada_contable_cuenta_contable_id_fk");

                entity.HasIndex(e => e.EntradaContableId)
                    .HasName("detalle_entrada_contable_entrada_contable_id_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CuentaContableId).HasColumnName("cuenta_contable_id");

                entity.Property(e => e.EntradaContableId).HasColumnName("entrada_contable_id");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.Monto).HasColumnName("monto");

                entity.Property(e => e.TipoMovimiento)
                    .IsRequired()
                    .HasColumnName("tipo_movimiento")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasComment("DB, CR");

                entity.HasOne(d => d.CuentaContable)
                    .WithMany(p => p.DetalleEntradaContable)
                    .HasForeignKey(d => d.CuentaContableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detalle_entrada_contable_cuenta_contable_id_fk");

                entity.HasOne(d => d.EntradaContable)
                    .WithMany(p => p.DetalleEntradaContable)
                    .HasForeignKey(d => d.EntradaContableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detalle_entrada_contable_entrada_contable_id_fk");
            });

            modelBuilder.Entity<EntradaContable>(entity =>
            {
                entity.ToTable("entrada_contable");

                entity.HasIndex(e => e.AuxiliarId)
                    .HasName("entrada_contable_auxiliar_id_fk");

                entity.HasIndex(e => e.MonedaId)
                    .HasName("entrada_contable_tipo_moneda_id_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuxiliarId).HasColumnName("auxiliar_id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(130)
                    .IsUnicode(false);

                //fecha by ignacio
                entity.Property(e => e.Fecha)
                    .IsRequired()
                    .HasColumnName("fecha");

                //monto by ignacio
                entity.Property(e => e.Monto)
                    .IsRequired()
                    .HasColumnName("monto");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                //moneda automatica by ignacio
                entity.Property(e => e.MonedaId)
                .HasColumnName("moneda_id");

                entity.HasOne(d => d.Auxiliar)
                    .WithMany(p => p.EntradaContable)
                    .HasForeignKey(d => d.AuxiliarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("entrada_contable_auxiliar_id_fk");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.EntradaContable)
                    .HasForeignKey(d => d.MonedaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("entrada_contable_tipo_moneda_id_fk");
            });

            modelBuilder.Entity<Moneda>(entity =>
            {
                entity.ToTable("moneda");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(130)
                    .IsUnicode(false);

                //codigo agregado by ignacio
                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.Tasa).HasColumnName("tasa");
            });

            modelBuilder.Entity<TipoCuenta>(entity =>
            {
                entity.ToTable("tipo_cuenta");

                //id a mano by ignacio
                entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(130)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.Origen)
                    .IsRequired()
                    .HasColumnName("origen")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasComment("DB, CR");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .HasColumnType("blob");

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("password_salt")
                    .HasColumnType("blob");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
