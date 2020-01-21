using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIServer.Controllers
{
    public class numerosController : ApiController
    {
        // GET: api/numeros
        public string Get()
        {
            return "Ingrese la lista de números";
        }

        // GET: api/numeros/5
        public string Get(string id)
        {
            int mVeces = 0;
            string mRetorna = "";
            string[] mNumeros = id.Split(new string[] { "," }, StringSplitOptions.None);

            for (int ii = 0; ii < mNumeros.Length; ii++)
            {
                //string mNumero = mNumeros[ii];

                int mVecesAhora = 0;
                for (int jj = 0; jj < mNumeros.Length; jj++)
                {
                    if (ii != jj)
                    {
                        if (mNumeros[ii] == mNumeros[jj]) mVecesAhora++;
                    }
                }

                if (mVecesAhora >= mVeces)
                {
                    mVeces = mVecesAhora;
                    mRetorna = mNumeros[ii];
                }
                    
            }

            return mRetorna;
        }

        // POST: api/numeros
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/numeros/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/numeros/5
        public void Delete(int id)
        {
        }
    }
}
