using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace sistemainventario.Models
{
    public class responsableHora
    {
        public int idtarea { get; set; }
        public int idarea { get; set; }
        public int? horas { get; set; }
        public List<int> responsables { get; set; }
        public ResponseModel guardar()
        {
            var rm = new ResponseModel();
            string sql;
            DateTime hoy = DateTime.Now;
            try
            {
                using (var ctx = new inventarioContext())
                {
                    foreach (var fila in this.responsables)
                    {
                        if (fila != 0)
                        {
                            sql = "INSERT INTO dbo.TareaResponsable (IdTarea,FechaAsignacionResponsable,IdResponsable) values(" + this.idtarea + ",'" + hoy + "'," + fila + ")";
                            ctx.Database.ExecuteSqlCommand(sql);
                        }                        
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
        public void EliminarResponsablesArea(int idArea) //eliminar reponsables de area excepto el encargado 
        {
            
                var rm = new ResponseModel();
                try
                {
                    using (var ctx = new inventarioContext())
                    {
                        //this.IdTarea = id;
                        ctx.Database.ExecuteSqlCommand("DELETE FROM dbo.TareaResponsable WHERE idTarea = " + id);
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
         
    }
}