namespace sistemainventario.Models
{
    using Newtonsoft.Json;
    using Proyecto.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;

    public partial class comentarios
    {
        [Key]
        public int IdComentario { get; set; }

        public int IdTarea { get; set; }

        [StringLength(1000)]
        public string Comentario { get; set; }

        public byte? Visible { get; set; }

        public DateTime? FechaHora { get; set; }

        public int IdUsuario { get; set; }

        public DateTime? FechaRegistro { get; set; }

    
        public int? ComentarioSistema { get; set; }
        [JsonIgnore]
        public virtual tareas tareas { get; set; }
        [JsonIgnore]
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
        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new inventarioContext())
                {
        
                    ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return rm;
        }
        public void comentarioSistema(int idtarea, string comentario)
        {
            DateTime hoy = DateTime.Now;
            int usuario = Helper.SessionHelper.GetIdUser();
            this.IdTarea = idtarea;
            this.Comentario = comentario;
            this.Visible = 1;
            this.FechaHora = hoy;
            this.IdUsuario = usuario;
            this.ComentarioSistema = 2; //2 comentario sistema
            this.Guardar();
        }
        
    }
}
