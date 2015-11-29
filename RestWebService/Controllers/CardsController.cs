using AliasGameBL.Models;
using AliasGameBL.Utillity;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace RestWebService.Controllers
{
    public class CardsController : ApiController
    {
        static CardStorage cardStorage;

        public static void SetCardStorage(CardStorage cs)
        {
            cardStorage = cs;
        }

        // GET api/values
        public IEnumerable<Card> Get()
        {
            return cardStorage.Cards;
        }

        // GET api/values/5
        public Card Get(int id)
        {
            if (id < 0 || id > (cardStorage.Cards.Count() - 1)) return null;

            return cardStorage.Cards.Skip(id).FirstOrDefault();
        }

        // POST api/values
        public void Post([FromBody]Card value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Card value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
