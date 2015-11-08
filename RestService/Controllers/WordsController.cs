using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestService.Controllers
{
    public class WordsController : ApiController
    {
        static string[] words;

        static WordsController()
        {
            var wordsProvider = new RestService.Models.Words();
            words = wordsProvider.GetAllWords();
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return words;
        }

        // GET api/values/5
        public string Get(int id)
        {
            if (id < 0 || id > (words.Length - 1)) return null;

            return words[id];
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}