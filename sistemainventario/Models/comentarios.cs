namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

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

    
        public int? ComentarioSistema { get; set; }

        public virtual tareas tareas { get; set; }

        public virtual usuariosSistema usuariosSistema { get; set; }
        public List<comentarios> Listar(int id)
        {
            List<comentarios> modelcomentarios = new List<comentarios>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    //enlaces = ctx.enlaces.ToList();
                    modelcomentarios = ctx.comentarios
                                        .Include("usuariosSistema")                                       
                                        .Where(x => x.IdTarea == id)
                                        .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return modelcomentarios;
        }
    }
}
