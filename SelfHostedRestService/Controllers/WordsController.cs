using SelfHostedRestService.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace SelfHostedRestService.Controllers
{
    public class WordsController : ApiController
    {
        static string[] words;

        static WordsController()
        {
            var wordsProvider = new Words();
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