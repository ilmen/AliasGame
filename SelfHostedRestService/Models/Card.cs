using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfHostedRestService.Models
{
    public class Card
    {
        #region Size static property
        private const int size = 10;

        public static int Size { get { return size; } }
        #endregion

        #region Words readonly property
        private readonly string[] words;

        public string[] Words { get { return words; } }
        #endregion

        #region Number readonly property
        private readonly int number;

        public int Number { get { return number; } } 
        #endregion
        
        public Card(int number, string[] wordsArray)
        {
            if (wordsArray.Length != size) throw new ArgumentOutOfRangeException("Карточки должны состоять из " + size + " слов");

            this.number = number;
            this.words = wordsArray;
        }
    }
}
