using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliasGameBL.Models
{
    public class Card
    {
        private readonly string[] words;
        private readonly int index;

        public string[] Words
        { get { return words; } }

        public int Index
        { get { return index; } } 
        
        public Card(int index, string[] cardWords)
        {
            if (cardWords == null) throw new ArgumentNullException("Пустая коллекция слов недопустима!");
            if (cardWords.Length == 0) throw new ArgumentException("Коллекция слов для карточки не может быть пустой!");

            this.index = index;
            this.words = cardWords;
        }
    }
}
