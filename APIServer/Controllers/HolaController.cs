using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIServer.Controllers
{
    public class HolaController : ApiController
    {
        // GET: api/Hola
        public string Get()
        {
            return "Hola Mundo!";
            //return new string[] { "value1", "value2" };
        }

        // GET: api/Hola/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Hola
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Hola/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Hola/5
        public void Delete(int id)
        {
        }
    }
}
