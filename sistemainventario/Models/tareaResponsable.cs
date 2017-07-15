namespace sistemainventario.Models
{
    using App_Start;
    using Helper;
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

        public ResponseModel Guardar()//guardar tarea responsable
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new inventarioContext())
                {
                     
                        //this.Eliminar(this.IdTarea);// eliminar a todos los responsables de la tarea // bloqueado por ahora
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
        public static void Eliminar(int? id)
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

        public static bool esResponsable(int _IdTarea)

        {            
            int _IdResponsable = SessionHelper.GetIdUser();
            _IdResponsable = responsable.ObtenerIdResponsableUsuario(_IdResponsable);
            tareaResponsable aux = new tareaResponsable();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    aux = ctx.tareaResponsable.Where(x => x.IdTarea == _IdTarea)
                                            .Where(x => x.IdResponsable == _IdResponsable)
                                            .SingleOrDefault();
                }
                if (aux == null)
                    return false;
                else
                    return true;
                    
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
