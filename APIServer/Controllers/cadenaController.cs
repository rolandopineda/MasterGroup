using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIServer.Controllers
{
    public class cadenaController : ApiController
    {
        // GET: api/cadena
        public string Get()
        {
            return "Debe enviar una cadena";
        }

        // GET: api/cadena/5
        public string Get(string cadena, int numero)
        {
            string mRetorna = "";

            int mContador = 0;
            string mCaracter = "";
            string mCaracterAnterior = "";

            for (int ii = 0; ii < cadena.Length; ii++)
            {
                mCaracter = cadena.Substring(ii, 1);
                if (mCaracter == mCaracterAnterior)
                {
                    mContador++;
                    if (mContador <= numero)
                    {
                        mRetorna = string.Format("{0}{1}", mRetorna, mCaracter);
                    }
                }
                else
                {
                    mContador = 1;
                    mRetorna = string.Format("{0}{1}", mRetorna, mCaracter);
                }

                mCaracterAnterior = mCaracter;
            }

            return mRetorna;
        }

        // POST: api/cadena
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/cadena/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/cadena/5
        public void Delete(int id)
        {
        }
    }
}
