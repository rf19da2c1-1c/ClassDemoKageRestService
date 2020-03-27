using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClassDemoKageLib.model;
using ClassDemoKageREST.DBUtil;

namespace ClassDemoKageREST.Controllers
{
    public class KagerController : ApiController
    {
        private ManagerKager mgr = new ManagerKager();

        // GET: api/Kager
        public IEnumerable<Kage> Get()
        {
            return mgr.HentAlle();
        }

        // GET: api/Kager/5
        public Kage Get(int id)
        {
            return mgr.HentEn(id);
        }

        // POST: api/Kager
        public bool Post([FromBody]Kage kage)
        {
            return mgr.OpretKage(kage);
        }

        // PUT: api/Kager/5
        public bool Put(int id, [FromBody]Kage kage)
        {
            return mgr.OpdaterKage(id, kage);
        }

        // DELETE: api/Kager/5
        public Kage Delete(int id)
        {
            return mgr.SletKage(id);
        }
    }
}
