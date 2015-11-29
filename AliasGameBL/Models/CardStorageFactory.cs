using AliasGameBL.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliasGameBL.Models
{
    public class CardStorageFactory
    {
        public CardStorage GetCardStorage()
        {
            var wordFact = new WordFactory();

            var shuffler = new Shuffler<string>();
            var cutter = new Cutter<string>();
            var cardFact = new CardFactory(10, shuffler, cutter);

            var words = wordFact.GetAllWords();
            var cards = cardFact.GetCards(words);

            return new CardStorage(words, cards);
        }
    }
}
