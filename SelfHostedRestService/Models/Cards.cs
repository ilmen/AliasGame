using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfHostedRestService.Models
{
    public class Cards
    {
        public Card[] GetCards(string[] words)
        {
            var activeWords = GetActiveWords(words);
            var randomWords = RandomizeWords(activeWords);
            
            var wordGroups = randomWords
                .Select((word, index) => new
                {
                    Word = word,
                    CardNumber = (int)(index / Card.Size) + 1
                })
                .GroupBy(x => x.CardNumber)
                .ToArray();

            return wordGroups
                .Select(x => new Card(x.Key, x.Select(w => w.Word).ToArray()))
                .ToArray();
        }

        private string[] GetActiveWords(string[] words)
        {
            if (words.Length < Card.Size) throw new ArgumentOutOfRangeException("Слов слишком мало даже для одной карточки! Минимум: " + Card.Size);

            // усекаем кол-во слов так, чтобы получилось целое число карточек
            var cardCount = (int)(words.Length / Card.Size);
            return words.Take(cardCount * Card.Size).ToArray();
        }

        private string[] RandomizeWords(string[] words)
        {
            var rnd = new Random(GetRandomSide());

            var rndWords = words
                .Select(x => new
                {
                    Word = x,
                    Rang = rnd.Next(words.Length)
                })
                .OrderBy(x => x.Rang)
                .ToArray();

            return rndWords.Select(x => x.Word).ToArray();
        }

        private int GetRandomSide()
        {
            return (int)DateTime.Now.Ticks;
        }
    }
}
