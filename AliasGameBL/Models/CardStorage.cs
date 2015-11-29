using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliasGameBL.Models
{
    public class CardStorage
    {
        private readonly IEnumerable<string> words;
        private readonly IEnumerable<Card> cards;

        public IEnumerable<string> Words { get { return words; } }

        public IEnumerable<Card> Cards { get { return cards; } }

        public CardStorage(IEnumerable<string> words, IEnumerable<Card> cards)
        {
            this.words = words;
            this.cards = cards;
        }
    }
}
