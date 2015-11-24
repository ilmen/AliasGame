using AliasGameBL.Models;
using AliasGameBL.Utillity;
using System.Collections.Generic;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CardsController : ApiController
    {
        static Card[] cards;

        static CardsController()
        {
            var wordProvider = new Words();
            var words = wordProvider.GetAllWords();
            var sizeProvider = new CardSizeProvider();

            var cardProvider = new Cards(sizeProvider.GetCardSize(), new Shuffler<string>(), new StringCutter());
            cards = cardProvider.GetCards(words);
        }

        // GET api/values
        public IEnumerable<Card> Get()
        {
            return cards;
        }

        // GET api/values/5
        public Card Get(int id)
        {
            if (id < 0 || id > (cards.Length - 1)) return null;

            return cards[id];
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
