namespace sistemainventario.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class inventarioContext : DbContext
    {
        public inventarioContext()
            : base("name=inventarioContext")
        {
        }

        public virtual DbSet<ciudades> ciudades { get; set; }
        public virtual DbSet<contratos> contratos { get; set; }
        public virtual DbSet<departamentos> departamentos { get; set; }
        public virtual DbSet<enlaces> enlaces { get; set; }
        public virtual DbSet<enlacesInternet> enlacesInternet { get; set; }
        public virtual DbSet<enlacesServicios> enlacesServicios { get; set; }
        public virtual DbSet<enlacesTecnologia> enlacesTecnologia { get; set; }
        public virtual DbSet<enlacesTipo> enlacesTipo { get; set; }
        public virtual DbSet<oficinas> oficinas { get; set; }
        public virtual DbSet<proveedores> proveedores { get; set; }
        public virtual DbSet<tipoOficina> tipoOficina { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ciudades>()
                .Property(e => e.ciudad)
                .IsUnicode(false);

            modelBuilder.Entity<ciudades>()
                .HasMany(e => e.oficinas)
                .WithRequired(e => e.ciudades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<contratos>()
                .Property(e => e.contrato)
                .IsUnicode(false);

            modelBuilder.Entity<contratos>()
                .Property(e => e.otros)
                .IsUnicode(false);

            modelBuilder.Entity<departamentos>()
                .Property(e => e.departamento)
                .IsUnicode(false);

            modelBuilder.Entity<departamentos>()
                .HasMany(e => e.ciudades)
                .WithRequired(e => e.departamentos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<enlaces>()
                .Property(e => e.velocidad)
                .IsUnicode(false);

            modelBuilder.Entity<enlaces>()
                .Property(e => e.observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<enlaces>()
                .HasMany(e => e.contratos)
                .WithRequired(e => e.enlaces)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<enlaces>()
                .HasMany(e => e.enlacesInternet)
                .WithRequired(e => e.enlaces)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<enlaces>()
                .HasMany(e => e.enlacesServicios)
                .WithRequired(e => e.enlaces)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<enlacesInternet>()
                .Property(e => e.planinternet)
                .IsUnicode(false);

            modelBuilder.Entity<enlacesServicios>()
                .Property(e => e.servicio)
                .IsUnicode(false);

            modelBuilder.Entity<enlacesServicios>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<enlacesTecnologia>()
                .Property(e => e.tecnologia)
                .IsUnicode(false);

            modelBuilder.Entity<enlacesTecnologia>()
                .HasMany(e => e.enlaces)
                .WithRequired(e => e.enlacesTecnologia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<enlacesTipo>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<enlacesTipo>()
                .HasMany(e => e.enlaces)
                .WithRequired(e => e.enlacesTipo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<oficinas>()
                .Property(e => e.nombre_oficina)
                .IsUnicode(false);

            modelBuilder.Entity<oficinas>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<oficinas>()
                .HasMany(e => e.enlaces)
                .WithRequired(e => e.oficinas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proveedores>()
                .Property(e => e.proveedor)
                .IsUnicode(false);

            modelBuilder.Entity<proveedores>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<proveedores>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<proveedores>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<proveedores>()
                .Property(e => e.web)
                .IsUnicode(false);

            modelBuilder.Entity<proveedores>()
                .HasMany(e => e.enlaces)
                .WithRequired(e => e.proveedores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipoOficina>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<tipoOficina>()
                .HasMany(e => e.oficinas)
                .WithRequired(e => e.tipoOficina)
                .WillCascadeOnDelete(false);
        }
    }
}
