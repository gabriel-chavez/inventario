using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace sistemainventario.App_Start
{
    public class Funciones
    {
        public static int TareaVisualizada()
        {
            int tarea = 0;
            try
            {
                int c = HttpContext.Current.Request.UrlReferrer.Segments.Count();
                tarea = Convert.ToInt32(HttpContext.Current.Request.UrlReferrer.Segments[c-1]); //para sistema                
                //tarea = Convert.ToInt32(HttpContext.Current.Request.UrlReferrer.Segments[4]); //para publicar
            }
            catch(Exception e)
            {
                throw;
            }

            return tarea;
        }
    }
}