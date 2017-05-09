namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class comentarios
    {
        [Key]
        public int IdComentario { get; set; }

        public int IdTarea { get; set; }

        [StringLength(700)]
        public string Comentario { get; set; }

        public byte? Visible { get; set; }

        public DateTime? FechaHora { get; set; }

        public int IdUsuario { get; set; }

        public DateTime? FechaRegistro { get; set; }

        [StringLength(200)]
        public string ComentarioSistema { get; set; }

        public virtual tareas tareas { get; set; }

        public virtual usuariosSistema usuariosSistema { get; set; }
    }
}
