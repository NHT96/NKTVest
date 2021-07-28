using NKTVest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NKTVest.Controllers
{
    public class TTLSPController : ApiController
    {
        NKTVDataContext data = new NKTVDataContext();
        [HttpPut]
        public IHttpActionResult Hide(string id)
        {
            var lsps = data.LOAISPs.SingleOrDefault(l=>l.MALOAI == id);
            if (lsps == null)
                return NotFound();
            lsps.TRANGTHAI = false;
            
            data.SubmitChanges();
            return Ok(id);
        }
    }
}
