using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static List<string> _theList = new List<string>();

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _theList;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return _theList.FirstOrDefault(s => s == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            _theList.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            // nonsensical in example
            // because the string is the same as a value
            _theList.Remove(id);
            _theList.Add(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _theList.Remove(id);
        }
    }
}
