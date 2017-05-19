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

    [Table("tareaResponsable")]
    public partial class tareaResponsable
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTareaResponsable { get; set; }

        public int? IdTarea { get; set; }

        public DateTime? FechaAsignacionResponsable { get; set; }

        public int IdResponsable { get; set; }

        public virtual responsable responsable { get; set; }

        public virtual tareas tareas { get; set; }

        public ResponseModel Guardar(bool editar)//guardar tarea responsable
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    if (editar) // si existe responsable eliminar
                    {
                        this.Eliminar(this.IdTarea);// eliminar a todos los responsables de la tarea
                        ctx.Entry(this).State = EntityState.Added;
                        ctx.SaveChanges();
                    }                   
                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return rm;
        }
        public void Eliminar(int? id)
        {
            if(id!=null)
            {
                var rm = new ResponseModel();
                try
                {
                    using (var ctx = new inventarioContext())
                    {
                        //this.IdTarea = id;
                        ctx.Database.ExecuteSqlCommand("DELETE FROM dbo.TareaResponsable WHERE idTarea = "+id);
                    //    ctx.SaveChanges();
                        
                        rm.SetResponse(true);
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
                //return rm;
            }
        }
        public bool SeEditoArea(int idArea, int IdTarea) //consulta si el area se edito
        {
            bool ret = true;
            if(IdTarea>0)
            {
                var modelTarea = new tareas();
                try
                {
                    using (var ctx = new inventarioContext())
                    {
                        //enlaces = ctx.enlaces.ToList();
                        modelTarea = ctx.tareas
                                            .Where(x => x.IdTarea == IdTarea)
                                            .SingleOrDefault();
                    }
                    if (modelTarea.IdArea == idArea)
                        ret = false;
                    else
                        ret = true;

                }
                catch (Exception)
                {

                    throw;
                }
            }
            
            return ret;
        }
    }
}
