namespace sistemainventario
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tareas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tareas()
        {
            comentarios = new HashSet<comentarios>();
            tareaResponsable = new HashSet<tareaResponsable>();
        }

        [Key]
        public int IdTarea { get; set; }

        public int IdArea { get; set; }

        public int IdPrioridad { get; set; }

        [StringLength(500)]
        public string TareaAsignada { get; set; }

        public int IdTipoTarea { get; set; }

        [StringLength(500)]
        public string Acciones { get; set; }

        public DateTime? FechaAsignacion { get; set; }

        public DateTime? FechaComprometida { get; set; }

        public int? HorasDiariasAsignadas { get; set; }

        public int IdEstadoTarea { get; set; }

        public DateTime? FechaCierre { get; set; }

        public int? Eficiencia { get; set; }

        public virtual areas areas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comentarios> comentarios { get; set; }

        public virtual estadoTarea estadoTarea { get; set; }

        public virtual prioridades prioridades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tareaResponsable> tareaResponsable { get; set; }

        public virtual tipoTareas tipoTareas { get; set; }
    }
}
