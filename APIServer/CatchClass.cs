using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIServer
{
    public class CatchClass
    {
        public static string ExMessage(Exception ex, string clase, string metodo)
        {
            string m = "";
            try
            {
                m = ex.StackTrace.Substring(ex.StackTrace.IndexOf("línea"));
            }
            catch
            {
                try
                {
                    m = ex.StackTrace.Substring(ex.StackTrace.IndexOf("line"));
                }
                catch
                {
                    //Nothing
                }
            }

            string mm = "";
            try
            {
                mm = ex.InnerException.Message;
            }
            catch
            {
                mm = "";
            }

            return string.Format("Error en {0} BackEnd {1}: {2} {3} {4}", clase, metodo, ex.Message, m, mm);
        }
    }
}