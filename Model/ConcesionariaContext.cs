using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ApiParcial.Model
{
    public partial class ConcesionariaContext : DbContext
    {
        public ConcesionariaContext()
        {
        }

        public ConcesionariaContext(DbContextOptions<ConcesionariaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auto> Autos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Auto>(entity =>
            {
                entity.HasKey(e => e.Patente);

                entity.Property(e => e.Patente).HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.Marca).HasMaxLength(50);

                entity.Property(e => e.Modelo).HasMaxLength(50);

                entity.Property(e => e.Motor).HasMaxLength(50);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Apellido).HasMaxLength(50);

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo postal");

                entity.Property(e => e.Direccion).HasMaxLength(50);

                entity.Property(e => e.Dni)
                    .HasMaxLength(50)
                    .HasColumnName("DNI");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Contrasenia)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.Property(e => e.VentaId).HasColumnName("VentaID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Patente)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ventas_Cliente");

                entity.HasOne(d => d.PatenteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.Patente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ventas_Autos");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
