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
                tarea = Convert.ToInt32(HttpContext.Current.Request.UrlReferrer.Segments[3]);
            }
            catch(Exception e)
            {
                throw;
            }

            return tarea;
        }
    }
}