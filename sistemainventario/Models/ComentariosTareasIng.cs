namespace sistemainventario.Models
{
    using Proyecto.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("ComentariosTareasIng")]
    public partial class ComentariosTareasIng
    {
        [Key]
        public int IdComentarioTareaIng { get; set; }

        public int IdTareaIng { get; set; }

        public int IdUsuario { get; set; }

        [StringLength(500)]
        public string Comentario { get; set; }
       
        public int ComSist { get; set; }

        public DateTime Fecha { get; set; }

        public virtual TareasIng TareasIng { get; set; }
        public virtual usuariosSistema usuariosSistema { get; set; }
        public List<ComentariosTareasIng> Listar(int id)
        {
            List<ComentariosTareasIng> modelcomentarios = new List<ComentariosTareasIng>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    //enlaces = ctx.enlaces.ToList();
                    modelcomentarios = ctx.ComentariosTareasIng
                                        .Where(x => x.IdTareaIng == id)
                                        .Include(x => x.usuariosSistema)
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
            this.IdTareaIng = idtarea;
            this.Comentario = comentario;            
            this.Fecha = hoy;
            this.IdUsuario = usuario;
            this.ComSist = 2; //2 comentario sistema
            this.Guardar();
        }
    }
}
