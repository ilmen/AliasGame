using AliasGameBL.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace RestWebService.Controllers
{
    public class WordsController : ApiController
    {
        static CardStorage cardStorage;

        public static void SetCardStorage(CardStorage cs)
        {
            cardStorage = cs;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return cardStorage.Words;
        }

        // GET api/values/5
        public string Get(int id)
        {
            if (id < 0 || id > (cardStorage.Words.Count() - 1)) return null;

            return cardStorage.Words.Skip(id).FirstOrDefault();
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