using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIServices.Controllers.MailManager
{
    public class MailManagerController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [Route("ActionRouteName")]
        public async Task<IHttpActionResult> Get()
        {
            //var data = await _core.ChangesAsync();
            //return Ok(data)
            //BadRequest()
            //HttpStatusCode.BadRequest
                //NotFound()
            return Ok(""); 
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}