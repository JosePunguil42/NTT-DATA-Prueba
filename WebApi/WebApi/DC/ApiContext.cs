using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApi.Models;

#nullable disable

namespace WebApi.DC
{
    public partial class ApiContext : DbContext
    {
        public ApiContext()
        {
        }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ClIdCliente)
                    .HasName("PK__p_client__0ED7ADE5C58CC3D6");

                entity.Property(e => e.ClIdCliente).HasColumnName("cl_id_cliente");

                entity.Property(e => e.ClContrasenia)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cl_contrasenia");

                entity.Property(e => e.ClDireccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cl_direccion");

                entity.Property(e => e.ClEdad)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("cl_edad");

                entity.Property(e => e.ClEstado).HasColumnName("cl_estado");

                entity.Property(e => e.ClGenero)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cl_genero");

                entity.Property(e => e.ClIdentificacion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cl_identificacion");

                entity.Property(e => e.ClNombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cl_nombre");

                entity.Property(e => e.ClTelefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cl_telefono");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.CuNumeroCuenta)
                    .HasName("PK__p_cuenta__5138EEC704D265DA");

                entity.Property(e => e.CuNumeroCuenta)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cu_numero_cuenta");

                entity.Property(e => e.CuEstado).HasColumnName("cu_estado");

                entity.Property(e => e.CuIdCliente).HasColumnName("cu_id_cliente");

                entity.Property(e => e.CuTipo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cu_tipo");

                entity.HasOne(d => d.CuIdClienteNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.CuIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__p_cuentas__cu_id__7C8A100F");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.MoIdMovimiento)
                    .HasName("PK__p_movimi__2D96FD745220BB54");

                entity.Property(e => e.MoIdMovimiento).HasColumnName("mo_id_movimiento");

                entity.Property(e => e.MoFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("mo_fecha");

                entity.Property(e => e.MoMovimiento)
                    .HasColumnType("money")
                    .HasColumnName("mo_movimiento");

                entity.Property(e => e.MoNumeroCuenta)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("mo_numero_cuenta");

                entity.Property(e => e.MoSaldoDisponible)
                    .HasColumnType("money")
                    .HasColumnName("mo_saldo_disponible");

                entity.Property(e => e.MoSaldoInicial)
                    .HasColumnType("money")
                    .HasColumnName("mo_saldo_inicial");

                entity.Property(e => e.MoTipoMovimiento)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("mo_tipo_movimiento");

                entity.HasOne(d => d.MoNumeroCuentaNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.MoNumeroCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__p_movimie__mo_nu__7F667CBA");
            })  ;
        }
    }
}
