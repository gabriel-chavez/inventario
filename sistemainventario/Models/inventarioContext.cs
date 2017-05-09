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
        /****ENLACES****/
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
        /*********/
        /***************TAREAS***************/
        
        public virtual DbSet<areas> areas { get; set; }
        public virtual DbSet<comentarios> comentarios { get; set; }
        public virtual DbSet<estadoTarea> estadoTarea { get; set; }
        public virtual DbSet<prioridades> prioridades { get; set; }
        public virtual DbSet<responsable> responsable { get; set; }
        public virtual DbSet<tareaResponsable> tareaResponsable { get; set; }
        public virtual DbSet<tareas> tareas { get; set; }
        public virtual DbSet<tipoTareas> tipoTareas { get; set; }
        public virtual DbSet<usuariosSistema> usuariosSistema { get; set; }

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

            /********************TAREAS**************/
            modelBuilder.Entity<areas>()
               .Property(e => e.Area)
               .IsUnicode(false);

            modelBuilder.Entity<areas>()
                .HasMany(e => e.tareas)
                .WithRequired(e => e.areas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<comentarios>()
                .Property(e => e.Comentario)
                .IsUnicode(false);

            modelBuilder.Entity<comentarios>()
                .Property(e => e.ComentarioSistema)
                .IsUnicode(false);

            modelBuilder.Entity<estadoTarea>()
                .Property(e => e.EstadoTarea1)
                .IsUnicode(false);

            modelBuilder.Entity<estadoTarea>()
                .HasMany(e => e.tareas)
                .WithRequired(e => e.estadoTarea)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<prioridades>()
                .Property(e => e.Prioridad)
                .IsUnicode(false);

            modelBuilder.Entity<prioridades>()
                .HasMany(e => e.tareas)
                .WithRequired(e => e.prioridades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<responsable>()
                .HasMany(e => e.tareaResponsable)
                .WithRequired(e => e.responsable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tareas>()
                .Property(e => e.TareaAsignada)
                .IsUnicode(false);

            modelBuilder.Entity<tareas>()
                .Property(e => e.Acciones)
                .IsUnicode(false);

            modelBuilder.Entity<tareas>()
                .HasMany(e => e.comentarios)
                .WithRequired(e => e.tareas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipoTareas>()
                .Property(e => e.TipoTarea)
                .IsUnicode(false);

            modelBuilder.Entity<tipoTareas>()
                .HasMany(e => e.tareas)
                .WithRequired(e => e.tipoTareas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuariosSistema>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<usuariosSistema>()
                .Property(e => e.Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuariosSistema>()
                .HasMany(e => e.comentarios)
                .WithRequired(e => e.usuariosSistema)
                .WillCascadeOnDelete(false);
        }
    }
}
