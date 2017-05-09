namespace sistemainventario
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

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
