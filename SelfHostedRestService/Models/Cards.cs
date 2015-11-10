using SelfHostedRestService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfHostedRestService.Models
{
    public class Cards
    {
        public IShuffler<string> Shuffler { get; set; }

        public int MaxWordsCountInOneCard { get; private set; }

        public Cards(int wordsCountInOneCard)
        {
            this.MaxWordsCountInOneCard = wordsCountInOneCard;

            this.Shuffler = new StringShuffler();
        }

        public Card[] GetCards(string[] words)
        {
            var activeWords = GetActiveWords(words);
            var randomWords = Shuffler.Shuffle(activeWords);
            
            var wordGroups = randomWords
                .Select((word, index) => new
                {
                    Word = word,
                    CardIndex = (int)(index / MaxWordsCountInOneCard)
                })
                .GroupBy(x => x.CardIndex)
                .ToArray();

            return wordGroups
                .Select(x => new Card(x.Key, x.Select(w => w.Word).ToArray()))
                .ToArray();
        }

        private string[] GetActiveWords(string[] words)
        {
            if (words.Length < MaxWordsCountInOneCard) throw new ArgumentOutOfRangeException("Слов слишком мало даже для одной карточки! Минимум: " + MaxWordsCountInOneCard);

            // усекаем кол-во слов так, чтобы получилось целое число карточек
            var cardCount = (int)(words.Length / MaxWordsCountInOneCard);
            return words.Take(cardCount * MaxWordsCountInOneCard).ToArray();
        }
    }
}
