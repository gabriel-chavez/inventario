namespace sistemainventario
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tareaResponsable")]
    public partial class tareaResponsable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTareaResponsable { get; set; }

        public int? IdTarea { get; set; }

        public DateTime? FechaAsignacionResponsable { get; set; }

        public int IdResponsable { get; set; }

        public virtual responsable responsable { get; set; }

        public virtual tareas tareas { get; set; }
    }
}
