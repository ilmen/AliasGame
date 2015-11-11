using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfHostedRestService.Models
{
    public class Card
    {
        #region Words readonly property
        private readonly string[] words;

        public string[] Words { get { return words; } }
        #endregion

        #region Index readonly property
        private readonly int index;

        public int Index { get { return index; } } 
        #endregion
        
        public Card(int index, string[] wordsArray)
        {
            if (wordsArray.Length == 0) throw new ArgumentOutOfRangeException("Коллекция слов для карточки не может быть пустой!");

            this.index = index;
            this.words = wordsArray;
        }
    }
}
