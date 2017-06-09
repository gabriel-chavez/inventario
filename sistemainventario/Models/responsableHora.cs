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
                    this.EliminarResponsablesArea();
                    foreach (var fila in this.responsables)
                    {
                        if (fila != 0)
                        {
                            sql = "INSERT INTO dbo.TareaResponsable (IdTarea,FechaAsignacionResponsable,IdResponsable) values(" + this.idtarea + ",'" + hoy + "'," + fila + ")";
                            ctx.Database.ExecuteSqlCommand(sql);
                        }                        
                    }
                    if(this.horas!=null && this.IsNumeric(this.horas.ToString()))
                    {
                        sql = "UPDATE dbo.tareas SET HorasDiariasAsignadas=" + this.horas + " Where idTarea=" + this.idtarea;
                        ctx.Database.ExecuteSqlCommand(sql);
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
        public Boolean IsNumeric(string valor)
        {
            int result;
            return int.TryParse(valor, out result);
        }
        public void EliminarResponsablesArea() //eliminar reponsables de area excepto el encargado 
        {
            var responsable = new responsable();
            int idResponsable = responsable.ObtenerIdResponsable(this.idarea);
            string sql;
                try
                {
                    using (var ctx = new inventarioContext())
                    {                    
                       sql = "DELETE FROM dbo.TareaResponsable WHERE idTarea = " + this.idtarea+ " and idResponsable<> "+idResponsable;
                       ctx.Database.ExecuteSqlCommand(sql);       
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
